using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pocom.BLL.Interfaces;
using Pocom.BLL.Models.Identity;
using Pocom.DAL.Entities;

namespace Pocom.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly UserManager<UserAccount> _userManager;
        private readonly SignInManager<UserAccount> _signInManager;
        private readonly IUserAuthService _userAccountService;

        public AccountController(UserManager<UserAccount> userManager, SignInManager<UserAccount> signInManager, IUserAuthService userAccountService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userAccountService = userAccountService;
        }

        [Authorize]
        [HttpPost("sign-out")]
        public async Task<IActionResult> Logout()
        {
            var user = User.Identity;
            if (user is not null && user.IsAuthenticated)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return Ok("You are not authorized now");
            }
            return StatusCode(401);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterViewModel userRegistration)
        {

            var userResult = await _userAccountService.RegisterUserAsync(userRegistration);
            return !userResult.Succeeded ? new BadRequestObjectResult(userResult) : StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] LoginViewModel user)
        {
            var loginResult = await _userAccountService.ValidateUserAsync(user);
            return !loginResult.Succeeded
                ? Unauthorized()
                : Ok(new { Token = await _userAccountService.CreateTokenAsync() });
        }

    }
}
