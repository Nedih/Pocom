using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Pocom.BLL.Interfaces;
using Pocom.BLL.Models.Identity;
using Pocom.DAL.Entities;
using Pocom.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Pocom.BLL.Services
{
    public class UserAuthService : IUserAuthService
    {
        private readonly UserManager<UserAccount> _userManager;
        //private readonly RoleManager<UserAccount> _roleManager;
        private readonly IConfiguration _configuration;
        private UserAccount? _user;

        public UserAuthService(UserManager<UserAccount> userManager, /*RoleManager<UserAccount> roleManager,*/ IConfiguration configuration)
        {
            _userManager = userManager;
            //_roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<IdentityResult> RegisterUserAsync(RegisterViewModel model)
        {
            UserAccount user = new UserAccount
            {
                Email = model.Email,
                UserName = model.Email,
                Name = model.Name,
                Login = model.Login,
                PhoneNumber = model.PhoneNumber,
                DateOfBirth = model.DateOfBirth
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            return result; 
        }

        public async Task<IdentityResult> ValidateUserAsync(LoginViewModel model)
        {
            _user = await _userManager.FindByNameAsync(model.Email);
            if (_user == null)
                return IdentityResult.Failed(new IdentityError { Description = "There is no account with such email", Code = "WrongEmail" });
            if (!await _userManager.CheckPasswordAsync(_user, model.Password))
                return IdentityResult.Failed(new IdentityError { Description = "You entered wrong password", Code = "WrongPassword" });
            if (await _userManager.IsLockedOutAsync(_user))
                return IdentityResult.Failed(new IdentityError { Description = "Your account is locked", Code = "UserLocked"});
            return IdentityResult.Success;
        }

        public async Task<string> CreateTokenAsync()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var jwtConfig = _configuration.GetSection("jwtConfig");
            var key = Encoding.UTF8.GetBytes(jwtConfig["Secret"]);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, _user.Email)
        };
            var roles = await _userManager.GetRolesAsync(_user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtConfig");
            var r = jwtSettings["validAudience"];
            var tokenOptions = new JwtSecurityToken
            (
            issuer: jwtSettings["validIssuer"],
            audience: "http://localhost:3000/",//jwtSettings["validAudienceFront"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expiresIn"])),
            signingCredentials: signingCredentials
            );
            return tokenOptions;
        }
    }
}
