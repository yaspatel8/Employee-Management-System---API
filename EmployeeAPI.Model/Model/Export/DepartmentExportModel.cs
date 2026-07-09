using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAPI.Model.Model.Export
{
    public class DepartmentExportModel
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public string IsActive { get; set; }
        public string IsDeleted { get; set; }
    }
}
