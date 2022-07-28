using Microsoft.EntityFrameworkCore;
using Proje.Business.Repositories.IRepository;
using Proje.DataAccess.Contexts;
using Proje.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proje.Business.Repositories.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ProjeContext context) : base(context)
        {
        }

        public async Task<List<Product>> GetAllProductWithCategory()
        {
            return await _context.Products
                .Include(c => c.Category).AsNoTracking().ToListAsync();
        }
    }
}
