using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pocom.BLL.Interfaces;
using Pocom.BLL.Models;
using Pocom.BLL.Models.Identity;
using Pocom.BLL.Services;
using Pocom.DAL.Entities;

namespace Pocom.Api.Controllers
{
    [Route("api/user")]
    public class UserController : Controller
    {
        IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
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
        public async Task<IdentityResult> Create([FromBody]RegisterViewModel user)
        {

            return await _userService.Create(user);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IdentityResult> Update(string id, [FromBody]UserDTO user)
        {

            return await _userService.Update(id, user);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("lock")]
        public async Task<IdentityResult> Lock(string id)
        {

            return await _userService.LockUser(id);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("unlock")]
        public async Task<IdentityResult> Unlock(string id)
        {

            return await _userService.UnLockUser(id);
        }

    }
}
