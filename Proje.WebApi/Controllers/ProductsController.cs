using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proje.Business.Services.IService;
using Proje.Entities;
using Proje.Entities.Dtos;

namespace Proje.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllAsync();
            var productdto = _mapper.Map<List<ProductListDto>>(products.ToList());
            return Ok(productdto);

        }


        [Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllProductWithCategory()
        {
            var product = await _productService.GetAllProductWithCategory();
            return Ok(product);
        }



        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            return Ok(product);
        }
        [Authorize(Roles = "Personel,Admin")]
        [HttpPost]
        public async Task<IActionResult> Save(ProductSaveDto productDto)
        {
            var pro = await _productService.AddAsync(_mapper.Map<Product>(productDto));
            var proDto=_mapper.Map<ProductSaveDto>(productDto);
            return Created(String.Empty, proDto);
        }

        [Authorize(Roles = "Personel,Admin")]
        [HttpPut]
        public async Task<IActionResult> Update(ProductSaveDto productDto)
        {

            await _productService.UpdateAsync(_mapper.Map<Product>(productDto));
            return NoContent();
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            await _productService.DeleteAsync(product);
            return NoContent();
        }
    }
}
