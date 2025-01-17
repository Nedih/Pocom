using Microsoft.AspNetCore.Identity;
using Pocom.BLL.Models.Identity;
using Pocom.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pocom.BLL.Interfaces
{
    public interface IUserAuthService
    {
        Task<IdentityResult> RegisterUserAsync(RegisterViewModel model);
        Task<IdentityResult> ValidateUserAsync(LoginViewModel model);
        Task<string> CreateTokenAsync();
        Task<TokenModel> CreateTokensAsync(LoginViewModel model);
        Task<TokenModel> RefreshTokensAsync(TokenModel tokenModel);
    }
}
