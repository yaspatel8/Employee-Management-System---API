using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EmployeeAPI.Model.Model
{
    public class UserModel : CommonModel
    {
        public int Id { get; set; }

        [Required,MinLength(2),MaxLength(30)]
        public string? FullName { get; set; }

        [Required, EmailAddress, RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address.")]
        public string Email { get; set; } = string.Empty;   
        [Required,MinLength(6)]
        public string PasswordHash { get; set; }
        public int RoleId { get; set; }
        public string? RoleName { get; set; }

    }
}