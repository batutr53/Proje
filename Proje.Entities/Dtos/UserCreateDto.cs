using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proje.Entities.Dtos
{
    public class UserCreateDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
    }
}
