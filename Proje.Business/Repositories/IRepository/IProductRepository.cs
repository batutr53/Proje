﻿using Proje.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proje.Business.Repositories.IRepository
{
    public interface IProductRepository:IRepository<Product>
    {
        Task<List<Product>> GetAllProductWithCategory();
    }
}
