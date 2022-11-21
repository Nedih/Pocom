using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pocom.BLL.Models
{
    public class ProfileDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }

        public static explicit operator ProfileDTO(UserDTO user)
        {
            return new ProfileDTO
            {
                Name = user.Name,
                Email = user.Email,
                Login = user.Login,
                DateOfBirth = user.DateOfBirth,
                PhoneNumber = user.PhoneNumber
            };
        }
    }
}
