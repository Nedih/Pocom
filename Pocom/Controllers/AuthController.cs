using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Principal;
using Pocom.DAL.Entities;

namespace Pocom.Api.Controllers
{
    public class AuthController : Controller
    { 
        private readonly Role userRole = new Role();


        [Authorize]
        [HttpGet("/data")]
        public IActionResult Data()
        {
            var data = new
            {
                message = "WIN"
            };
            return Json(data);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserAccount loginData)
        {
            UserAccount? user = new UserAccount();//Users.FirstOrDefault(u => u.Login == loginData.Email && u.Password == loginData.PasswordHash);
            if (user is null) return StatusCode(401);

            var claims = new List<Claim> { /*new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                                            new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name)*/};
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookie");
            IIdentity identity = claimsIdentity;
            await HttpContext.SignInAsync("Cookies", new ClaimsPrincipal(claimsIdentity));
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {

            var user = User.Identity;
            if (user is not null && user.IsAuthenticated)
            {
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Login");
            }
            return StatusCode(401);

        }

    }
}
