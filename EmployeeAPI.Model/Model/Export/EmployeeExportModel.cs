using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAPI.Model.Model.Export
{
    public class EmployeeExportModel
    {
        public int EmployeeId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public decimal? Salary { get; set; }
        public string? PhoneNumber { get; set; }
        public string? DepartmentName { get; set; }
        public string? IsActive { get; set; } 
        public string? IsDeleted { get; set; } 
        public DateTime? DateOfJoining { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
