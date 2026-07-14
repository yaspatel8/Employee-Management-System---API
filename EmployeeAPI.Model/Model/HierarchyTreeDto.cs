using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAPI.Model.Model
{
    public class HierarchyTreeDto
    {
        public int EmployeePositionId { get; set; }

        public int? ReportsToEmployeePositionId { get; set; }

        public int EmployeeId { get; set; }

        public string FullName { get; set; } = string.Empty;

        public int? DepartmentId { get; set; }

        public string? DepartmentName { get; set; }

        public int? PositionId { get; set; }

        public string? PositionName { get; set; }

        public int Level { get; set; }

        public string? ProfileImage { get; set; }
    }
}
