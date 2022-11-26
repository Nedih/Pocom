using AutoMapper;
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
        private readonly IMapper _mapper;

        public UserService(IRepository<UserAccount> repo, UserManager<UserAccount> userManager, AutoMapper.IMapper mapper)
        {
            this.repo = repo;
            this.userManager = userManager;
            _mapper = mapper;
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            return _mapper.Map<List<UserDTO>>(userManager.Users.ToList());
        }

        public IEnumerable<ProfileDTO> GetUsersList()
        {
            var userModels = _mapper.Map<List<UserDTO>>(userManager.Users.ToList());
            var profileModels = new List<ProfileDTO>();
            foreach (var user in userModels)
                profileModels.Add((ProfileDTO)user);
            return profileModels;
        }

        public async Task<UserDTO> GetUser(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user != null)
            {
                return _mapper.Map<UserDTO>(user);
            }
            else return new UserDTO();
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

            /*user = */_mapper.Map(userDto, user);

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
                user = _mapper.Map<UserAccount>(userDto);
                var result = await userManager.CreateAsync(user, userDto.Password);
                if (result.Errors.Count() == 0)
                {
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
