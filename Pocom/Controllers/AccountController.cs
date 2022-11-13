using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pocom.BLL.Models.Identity;
using Pocom.DAL.Entities;

namespace Pocom.Api.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<UserAccount> _userManager;
        private readonly SignInManager<UserAccount> _signInManager;

        public AccountController(UserManager<UserAccount> userManager, SignInManager<UserAccount> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Route("sign-up")]
        [HttpPost]
        public async Task<bool> Register([FromBody]RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserAccount user = new UserAccount { Email = model.Email, UserName = model.Email, Name = model.Name,
                    Login = model.Login, PhoneNumber = model.PhoneNumber, DateOfBirth = model.DateOfBirth };
                // добавляем пользователя
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // установка куки
                    await _signInManager.SignInAsync(user, false);
                    return true;
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return false;
        }

        [Route("sign-in")]
        [HttpPost]      
        public async Task<bool> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                if (result.Succeeded)
                {
                    return true;
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return false;
        }

        [Route("sign-out")]
        [HttpPost]
        public async Task<bool> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return true;
        }

    }
}
