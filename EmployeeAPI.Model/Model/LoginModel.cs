using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAPI.Model.Model
{
    public class LoginModel : CommonModel
    {
        public int UserId { get; set; }

        public string? FullName { get; set; } = string.Empty;

        [Required,EmailAddress]
        public string Email { get; set; }

        [Required,MinLength(6)]
        public string PasswordHash { get; set; }

        public string? RoleName { get; set; }
    }
    public class PasswordResetModel : LoginModel
    {
        public int PasswordResetId { get; set; }
        [Required]
        public int UserId { get; set; }

        public string Token { get; set; } = string.Empty;

        public DateTime ExpiryTime { get; set; }

        public bool IsUsed { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? UsedOn { get; set; }
    }

}
