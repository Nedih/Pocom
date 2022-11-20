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
                /*if (await userManager.IsInRoleAsync(user, "User"))
                    user.Role = "user";
                if (await userManager.IsInRoleAsync(user, "Admin"))
                    user.Role = "admin";*/
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
            else return null;//IdentityResult.Failed(new IdentityError { Description = "There is no user with such ID.", Code = "WrongID" });
        }

        public async Task<IdentityResult> Update(string id, UserDTO userDto)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "There is no user with such ID.", Code = "WrongID" });

            user.Email = userDto.Email;
            user.Name = userDto.Name;
            user.UserName = userDto.Email;
            user.Login = userDto.Login;
            user.PhoneNumber = userDto.PhoneNumber;
            user.DateOfBirth = userDto.DateOfBirth;
 
            /*var roles = userManager.GetRoles(user.Id);

            if (userDto.Role != "" && userDto.Role != null && !roles.Contains(userDto.Role) && roleManager.RoleExists(userDto.Role))
                await userManager.AddToRoleAsync(user.Id, userDto.Role);*/

            IdentityResult result = await userManager.UpdateAsync(user);
            await repo.SaveAsync();

            return result;
        }

        public async Task<IdentityResult> Create(RegisterViewModel userDto)
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
