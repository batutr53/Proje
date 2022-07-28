using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proje.Business.Services.IService;

namespace Proje.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _roleService.GetAllAsync();
            return Ok(products);

        }
    }
}
