using Microsoft.AspNetCore.Identity;
using Pocom.BLL.Models;
using Pocom.BLL.Models.Identity;

namespace Pocom.BLL.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserDTO> GetUsers();
        IEnumerable<ProfileDTO> GetUsersList();
        Task<UserDTO> GetUser(string email);
        Task<IdentityResult> UpdateUser(string id, UserDTO userDto);
        Task<IdentityResult> UpdateUser(string email, ProfileDTO userDto);
        Task<IdentityResult> CreateUser(RegisterViewModel userDto);
        Task<IdentityResult> UpdatePassword(string email, ChangePasswordViewModel model);
        Task<IdentityResult> UpdateEmail(string currentEmail, string newEmail);
        Task<IdentityResult> LockUser(string id);
        Task<IdentityResult> UnLockUser(string id);
    }
}
