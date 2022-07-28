using Proje.Business.Repositories.IRepository;
using Proje.Business.Services.IService;
using Proje.Business.UnitOfWorks;
using Proje.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proje.Business.Services.Service
{
    public class CategoryService : Service<Category>, ICategoryService
    {
        public CategoryService(IRepository<Category> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}