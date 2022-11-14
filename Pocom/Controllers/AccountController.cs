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

        /*[Route("sign-up")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody]RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserAccount user = new UserAccount { Email = model.Email, UserName = model.Email, Name = model.Name,
                    Login = model.Login, PhoneNumber = model.PhoneNumber, DateOfBirth = model.DateOfBirth };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return StatusCode(201);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return StatusCode(401);
        }

        [Route("sign-in")]
        [HttpPost]      
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                if (result.Succeeded)
                {
                    return StatusCode(200);
                }
                else
                {
                    ModelState.AddModelError("", "Wrong username and (or) password");
                }
            }
            return StatusCode(401);
        }*/

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
            return !await _userAccountService.ValidateUserAsync(user)
                ? Unauthorized()
                : Ok(new { Token = await _userAccountService.CreateTokenAsync() });
        }

    }
}
