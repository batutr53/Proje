using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Proje.WebUi.Models;
using System.Text.Json;

namespace Proje.WebUi.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
       
        public async Task<IActionResult> GetAll()
        {
            var client = this.CreateClient();
            var response = await client.GetAsync("https://localhost:7227/api/Products/GetAllProductWithCategory");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
              var list =  JsonSerializer.Deserialize<List<ProductGetAllModel>>(jsonString, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
                return View(list);
            }
            return View();
        }
       
        public async Task<IActionResult> REmove(int id)
        {
            var client = this.CreateClient();
          
            await client.DeleteAsync($"https://localhost:7227/api/Products/{id}");
         
            return RedirectToAction("GetAll");
        }
       
        public async Task<IActionResult> Create()
        {
            var client = this.CreateClient();
            var response = await client.GetAsync("https://localhost:7227/api/Categories");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var list = JsonSerializer.Deserialize<List<CategoryListModel>>(jsonString, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
              ViewBag.Category = new SelectList(list, "Id", "Name");
                return View(new ProductCreateModel());
            }
            return View();
        }

        [HttpPost]
     
        public async Task<IActionResult> Create(ProductCreateModel model)
        {
           
            var client = this.CreateClient();
             await client.PostAsJsonAsync("https://localhost:7227/api/Products/",model);
            if (ModelState.IsValid)
            {
                return RedirectToAction("GetAll");
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var client = this.CreateClient();
            var response = await client.GetAsync($"https://localhost:7227/api/Products/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var list = JsonSerializer.Deserialize<ProductGetAllModel>(jsonString, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
                var cat = await client.GetAsync("https://localhost:7227/api/Categories");
                if (cat.IsSuccessStatusCode)
                {
                    var jsonStrings = await cat.Content.ReadAsStringAsync();
                    var lists = JsonSerializer.Deserialize<List<CategoryListModel>>(jsonStrings, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                    ViewBag.Category = new SelectList(lists, "Id", "Name");
                    return View(list);
            }

        
            }
                return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductCreateModel model)
        {
            var client = this.CreateClient();

            var update = await client.PutAsJsonAsync<ProductCreateModel>("https://localhost:7227/api/Products/", model);
            
            if (update.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAll");
            }



            return View(model);
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
