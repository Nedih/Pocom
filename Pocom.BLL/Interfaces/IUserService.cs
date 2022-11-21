using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pocom.BLL.Models;
using Pocom.BLL.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Pocom.BLL.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserDTO> GetUsers();
        Task<UserDTO> GetUser(string email);
        Task<IdentityResult> Update(string id, UserDTO userDto);
        Task<IdentityResult> Create(RegisterViewModel userDto);
        Task<IdentityResult> LockUser(string id);
        Task<IdentityResult> UnLockUser(string id);
    }
}
