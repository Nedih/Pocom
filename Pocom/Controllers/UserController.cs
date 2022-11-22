using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pocom.BLL.Interfaces;
using Pocom.BLL.Models;
using Pocom.BLL.Models.Identity;

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

        //[Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public IEnumerable<UserDTO> Get()
        {
            return _userService.GetUsers();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetUser(string email)
        {
            var user = await _userService.GetUser(email);
            if (user == null)
                return NotFound("No such user with this email");
            return Ok(user);
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetUserProfile()
        {
            string? email = User.Identity.Name;
            if (string.IsNullOrEmpty(email))
                return BadRequest("Email is empty");
            var profile = (ProfileDTO)await _userService.GetUser(email);
            if (profile == null)
                return NotFound("No such user with this email");
            return Ok(profile);
        }

        [Authorize]
        [HttpPut("profile")]
        public async Task<IdentityResult> UpdateUserProfile([FromBody] ProfileDTO user)
        {
            string? email = User.Identity.Name;
            return await _userService.UpdateUser(email, user);
        }

        [Authorize]
        [HttpPut("email")]
        public async Task<IdentityResult> UpdateEmail([FromBody] string email)
        {
            string? currentEmail = User.Identity.Name;
            return await _userService.UpdateEmail(currentEmail, email);
        }

        [Authorize]
        [HttpPut("password")]
        public async Task<IdentityResult> UpdatePassword([FromBody] ChangePasswordViewModel model)
        {
            string? email = User.Identity.Name;
            return await _userService.UpdatePassword(email, model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IdentityResult> Create([FromBody]RegisterViewModel user)
        {

            return await _userService.CreateUser(user);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IdentityResult> Update(string id, [FromBody]UserDTO user)
        {

            return await _userService.UpdateUser(id, user);
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
