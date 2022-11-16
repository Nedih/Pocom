﻿using Microsoft.AspNetCore.Identity;
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

namespace Pocom.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<UserAccount> userManager;

        private readonly RoleManager<UserAccount> roleManager;

        private readonly IRepository<UserAccount> repo;

        public UserService(IRepository<UserAccount> repo, UserManager<UserAccount> userManager, RoleManager<UserAccount> roleManager)
        {
            this.repo = repo;
            this.userManager = userManager;
            this.roleManager = roleManager;
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

        public async Task<bool> Update(string id, UserDTO userDto)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
                return false;
            user.Email = userDto.Email;
            user.Name = userDto.Name;
            /*var roles = userManager.GetRoles(user.Id);

            if (userDto.Role != "" && userDto.Role != null && !roles.Contains(userDto.Role) && roleManager.RoleExists(userDto.Role))
                await userManager.AddToRoleAsync(user.Id, userDto.Role);*/

            try
            {
                await userManager.UpdateAsync(user);
                await repo.SaveAsync();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> Create(RegisterViewModel userDto)
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
                if (result.Errors.Count() > 0)
                    //return new OperationDetails(false, result.Errors.FirstOrDefault());
                    return false;
                await userManager.AddToRoleAsync(user, "User");
                await repo.SaveAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
