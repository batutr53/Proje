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
    public class UserService : Service<User>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IRepository<User> repository, IUnitOfWork unitOfWork, IUserRepository userRepository) : base(repository, unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<User> Authenticate(string mail, string password)
        {
            var user = await _userRepository.Authenticate(mail, password);
            return user;
        }

        public async Task<bool> IsUniqueUser(string mail)
        {
            return await _userRepository.IsUniqueUser(mail);
        }

        public async Task<User> Register(string mail, string password,int rolId)
        {
            var user = await _userRepository.Register(mail, password, rolId);
            await _unitOfWork.CommitAsync();
            return user;
        }
    }
}
