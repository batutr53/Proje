using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proje.Business.Services.IService;
using Proje.Entities;

namespace Proje.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Save(Category category)
        {
            var cat = await _categoryService.AddAsync(category);
            return Created(String.Empty,cat);
        }

        [HttpPatch]
        public async Task<IActionResult> Update(Category category)
        {
         
            await _categoryService.UpdateAsync(category);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            await _categoryService.DeleteAsync(category);
            return NoContent();
        }
    }
}
