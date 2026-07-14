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
        
        public string? PhoneNumber { get; set; }

        public decimal? Salary { get; set; }
        public int? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public string Email { get; set; } = string.Empty;

        public int RoleId { get; set; }

        public string PasswordHash { get; set; } = string.Empty;
        public IFormFile? ProfilePicture { get; set; }
        public string? ProfileImage { get; set; }
        public int? PositionId { get; set; }
        public string? PositionName { get; set; }
        public int? ReportsToEmployeePositionId { get; set; }

    }
    public class EmployeeWithDepartmentModel : EmployeeModel
    {
        public string DepartmentName { get; set; }
    }
    public class BulkDeleteEmployeeModel : CommonModel
    {
       public List<int> EmployeeIds { get; set; } = new();
    }
    public class BulkUpdateEmployeeModel : CommonModel
    {
        public int EmployeeId { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public decimal? Salary { get; set; }

        public int? DepartmentId { get; set; }

        public bool IsActive { get; set; }
    }
}
