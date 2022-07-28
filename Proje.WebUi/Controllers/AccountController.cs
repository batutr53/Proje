using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Proje.WebUi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace Proje.WebUi.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult SignIn()
        {
            return View();
        
        }


        [HttpPost]
        public async Task<IActionResult> SignIn(UserLoginModel model)
        {
            var client = _httpClientFactory.CreateClient();
            var requestContent = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7227/api/Users/Authenticate", requestContent);
             if (response.IsSuccessStatusCode)
            {
              var jsonData = await response.Content.ReadAsStringAsync();
                var tokenModel = JsonSerializer.Deserialize<JwtResponseModel>(jsonData, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
               var token =  handler.ReadJwtToken(tokenModel?.Token);

                if (token !=null)
                {
                      var roles = token.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value);
                       if (roles.Contains("Admin"))
                       {
                        RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        RedirectToAction("Account", "SignIn");
                    }
                    var claims = new List<Claim> {
                    new Claim("accessToken", tokenModel?.Token==null?"":tokenModel.Token),
                    new Claim(ClaimTypes.NameIdentifier, model.Email),
                    new Claim(ClaimTypes.Name, model.Email)
                };
                 
                    ClaimsIdentity identity = new ClaimsIdentity(claims,JwtBearerDefaults.AuthenticationScheme);
                    var authProps = new AuthenticationProperties
                    {
                        AllowRefresh = false,
                        IsPersistent = true
                    };
                    await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), authProps);
             return RedirectToAction("Index", "Home");
                        }
                else
                {
                    ModelState.AddModelError("", "Email veya Şifre Yanlış");
                    return View(model);

                }
                
            }
            else
            {
                ModelState.AddModelError("", "Email veya Şifre Yanlışş");
                return View(model);
            }
          
        }

        public async Task<IActionResult> Register()
        {
            var client = this.CreateClient();
            var response = await client.GetAsync("https://localhost:7227/api/Role");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var list = JsonSerializer.Deserialize<List<RoleListModel>>(jsonString, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
                ViewBag.Role = new SelectList(list, "Id", "Name");
                return View(new UserRegisterModel());
            }

            return View(new UserRegisterModel());
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterModel user)
        {
            var client = this.CreateClient();
            await client.PostAsJsonAsync("https://localhost:7227/api/Users/Register", user);
            if (ModelState.IsValid)
            {
                return RedirectToAction("SignIn");
            }
            return View(user);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("SignIn");
        }
        private HttpClient CreateClient()
        {
            var client = _httpClientFactory.CreateClient();
            var token = User.Claims.SingleOrDefault(x => x.Type == "accessToken")?.Value;
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            return client;
        }
    }
}
