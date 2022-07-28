using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proje.Business.Services.IService;
using Proje.Entities;
using Proje.Entities.Dtos;
using System.Security.Claims;

namespace Proje.WebApi.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Authenticate(UserAuthDto user)
        {
            var users = await _userService.Authenticate(user.Email, user.Password);
            var userDto = _mapper.Map<UserAuthDto>(users);
            return Ok(userDto);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Register(UserCreateDto userRegister)
        {

            bool userBool = await _userService.IsUniqueUser(userRegister.Email);
            if (!userBool)
            {
                return BadRequest(new { message = "Email zaten mevcut." });
            }
            var user = _userService.Register(userRegister.Email, userRegister.Password,userRegister.RoleId);

            return Ok(userRegister);

        }
    }
}
