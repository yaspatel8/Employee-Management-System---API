using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAPI.Model.Model
{
    public class EmployeeModel : CommonModel
    {

        public int EmployeeId { get; set; }

        public int? UserId { get; set; }

        public string? FullName { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }

        public decimal Salary { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        public string Email { get; set; } = string.Empty;

        public int RoleId { get; set; }

        public string PasswordHash { get; set; } = string.Empty;
        public IFormFile? ProfilePicture { get; set; }
        public string? ProfileImage { get; set; }

    }
    public class EmployeeWithDepartmentModel : EmployeeModel
    {
        public string DepartmentName { get; set; }
    }
}
