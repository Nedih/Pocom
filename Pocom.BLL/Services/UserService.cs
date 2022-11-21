using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Pocom.BLL.Interfaces;
using Pocom.BLL.Models;
using Pocom.BLL.Models.Identity;
using Pocom.DAL.Entities;
using Pocom.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Pocom.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<UserAccount> userManager;

        private readonly IRepository<UserAccount> repo;

        public UserService(IRepository<UserAccount> repo, UserManager<UserAccount> userManager)
        {
            this.repo = repo;
            this.userManager = userManager;
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            var users = userManager.Users.ToList();
            var userModels = new List<UserDTO>();
            foreach (var user in users)
            {
                UserDTO userModel = new UserDTO
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Login = user.Login,
                    DateOfBirth = user.DateOfBirth,
                    PhoneNumber = user.PhoneNumber
                };

                userModels.Add(userModel);
            }
            return userModels;
        }

        public async Task<UserDTO> GetUser(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user != null)
            {
                return new UserDTO
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Login = user.Login,
                    DateOfBirth = user.DateOfBirth,
                    PhoneNumber = user.PhoneNumber
                };
            }
            else return null;
        }

        public async Task<IdentityResult> UpdateUser(string id, UserDTO userDto)
        {
            var user = await userManager.FindByIdAsync(id);
            return await Update(user, userDto);
        }

        public async Task<IdentityResult> UpdateUser(string email, ProfileDTO profile)
        {
            var user = await userManager.FindByEmailAsync(email);
            return await Update(user, new UserDTO(user.Id, email, profile));
        }

        public async Task<IdentityResult> Update(UserAccount user, UserDTO userDto)
        {
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "There is no such user.", Code = "WrongID" });

            user.Email = userDto.Email;
            user.Name = userDto.Name;
            user.UserName = userDto.Email;
            user.Login = userDto.Login;
            user.PhoneNumber = userDto.PhoneNumber;
            user.DateOfBirth = userDto.DateOfBirth;

            IdentityResult result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
                await repo.SaveAsync();

            return result;
        }

        public async Task<IdentityResult> UpdatePassword(string email, ChangePasswordViewModel model)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "There is no user with this Email.", Code = "WrongEmail" });
            IdentityResult result = await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            return result;
        }

        public async Task<IdentityResult> UpdateEmail(string currentEmail, string newEmail)
        {
            
            var user = await userManager.FindByEmailAsync(currentEmail);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "There is no user with this Email.", Code = "WrongEmail" });
            var token = await userManager.GenerateChangeEmailTokenAsync(user, newEmail);
            IdentityResult result = await userManager.ChangeEmailAsync(user, newEmail, token);

            return result;
        }

        public async Task<IdentityResult> CreateUser(RegisterViewModel userDto)
        {
            UserAccount user = await userManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new UserAccount
                {
                    Email = userDto.Email,
                    UserName = userDto.Email,
                    Name = userDto.Name,
                    Login = userDto.Login,
                    PhoneNumber = userDto.PhoneNumber,
                    DateOfBirth = userDto.DateOfBirth
                };
                var result = await userManager.CreateAsync(user, userDto.Password);
                if (result.Errors.Count() == 0) { 
                    await userManager.AddToRoleAsync(user, "User");
                    await repo.SaveAsync();
                }
                return result;
            }
            else
            {
                return IdentityResult.Failed(new IdentityError { Description = "This email have already been registered.", Code = "RegisteredEmail" });
            }           
        }

        public async Task<IdentityResult> LockUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "There is no user with such ID.", Code = "WrongID" });
            return await userManager.SetLockoutEndDateAsync(user, DateTime.Now.AddYears(1));
        }

        public async Task<IdentityResult> UnLockUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "There is no user with such ID.", Code = "WrongID" });
            return await userManager.SetLockoutEndDateAsync(user, DateTime.Now);
        }
    }
}
