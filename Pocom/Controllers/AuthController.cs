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
        [Authorize(Roles = "Admin")]
        [HttpGet("/data")]
        public IActionResult Data()
        {
            var data = new
            {
                message = "WIN"
            };
            return Json(data);
        }
    }
}
