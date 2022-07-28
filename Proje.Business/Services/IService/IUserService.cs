using Proje.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proje.Business.Services.IService
{
    public interface IUserService:IService<User>
    {
        Task<bool> IsUniqueUser(string mail);
        Task<User> Authenticate(string mail, string password);
        Task<User> Register(string mail, string password, int rolId);
    }
}
