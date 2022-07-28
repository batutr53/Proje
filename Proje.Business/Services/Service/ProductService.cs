using AutoMapper;
using Proje.Business.Repositories.IRepository;
using Proje.Business.Services.IService;
using Proje.Business.UnitOfWorks;
using Proje.Entities;
using Proje.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proje.Business.Services.Service
{
    public class ProductService : Service<Product>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        
        public ProductService(IRepository<Product> repository, IUnitOfWork unitOfWork, IMapper mapper, IProductRepository productRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<List<ProductWithCategoryDto>> GetAllProductWithCategory()
        {
           var products = await _productRepository.GetAllProductWithCategory();
            var productDto =  _mapper.Map<List<ProductWithCategoryDto>>(products).ToList();
            return productDto;
        }
    }
}
