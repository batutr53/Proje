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
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(ProjeContext context) : base(context)
        {
        }
    }
}
