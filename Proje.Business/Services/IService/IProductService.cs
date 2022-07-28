using Proje.Entities;
using Proje.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proje.Business.Services.IService
{
    public interface IProductService:IService<Product>
    {
        Task<List<ProductWithCategoryDto>> GetAllProductWithCategory();
    }
}
