using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Proje.Business.Repositories.IRepository;
using Proje.DataAccess.Contexts;
using Proje.Entities;
using Proje.SharedTools;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Proje.Business.Repositories.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private ProjeContext _context;
        private readonly AppSettings _appSettings;
        public UserRepository(ProjeContext context, IOptions<AppSettings>appSettings) : base(context)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        public async Task<User> Authenticate(string mail, string password)
        {
          
           
            var user = _context.Users.SingleOrDefault(x => x.Email == mail && x.Password == password);
            user.Role = _context.Roles.Where(u => u.Id == user.RoleId).FirstOrDefault();
            if (user == null)
            {
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var claims = new List<Claim>();
           /* foreach (var role in user.UserRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Role.Name));
            }*/
            var tokendDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                    {
                         new Claim(ClaimTypes.Email,user.Email),
                         new Claim(ClaimTypes.Role,user.Role.Name),
                    
                    }),    
            
            Expires = DateTime.UtcNow.AddDays(5),
                SigningCredentials = new SigningCredentials(
                         new SymmetricSecurityKey(key),
                         SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokendDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            user.Password = "";

            return user;
        }

        public async Task<bool> IsUniqueUser(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (user == null)
            {
                return true;
            }
            return false;
        }

        public async Task<User> Register(string mail, string password,int rolId)
        {
            User user = new User()
            {
                Email = mail,
                Password = password,
                RoleId = rolId
            }; 
        
            await _context.Users.AddAsync(user);
            return user;
        }

    
    }
}
