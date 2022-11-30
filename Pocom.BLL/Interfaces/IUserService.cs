using Microsoft.AspNetCore.Identity;
using Pocom.BLL.Models;
using Pocom.BLL.Models.Identity;

namespace Pocom.BLL.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserDTO> GetUsers();
        IEnumerable<ProfileDTO> GetUsersList();
        Task<UserDTO> GetUser(string id);
        public UserDTO GetUserByLogin(string login);
        Task<IdentityResult> UpdateUser(string id, UserDTO userDto);
        Task<IdentityResult> UpdateUser(string id, ProfileDTO userDto);
        Task<IdentityResult> CreateUser(RegisterViewModel userDto);
        Task<IdentityResult> UpdatePassword(string id, ChangePasswordViewModel model);
        Task<IdentityResult> UpdateEmail(string currentEmail, string newEmail);
        Task<IdentityResult> LockUser(string id);
        Task<IdentityResult> UnLockUser(string id);
    }
}
