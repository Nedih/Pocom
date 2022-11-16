using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pocom.BLL.Models;
using Pocom.BLL.Models.Identity;
using Pocom.BLL.Services;
using Pocom.DAL.Entities;

namespace Pocom.Api.Controllers
{
    [Route("api/user")]
    public class UserController : Controller
    {
        UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IEnumerable<UserDTO> Get()
        {

            return _userService.GetUsers();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<UserDTO> GetUser(string email)
        {

            return await _userService.GetUser(email);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<bool> Create(RegisterViewModel user)
        {

            return await _userService.Create(user);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<bool> Update(string id, UserDTO user)
        {

            return await _userService.Update(id, user);
        }

    }
}
