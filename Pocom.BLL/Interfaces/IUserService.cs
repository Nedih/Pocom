using Microsoft.AspNetCore.Identity;
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
        Task<bool> Update(string id, UserDTO userDto);
        Task<bool> Create(RegisterViewModel userDto);
    }
}
