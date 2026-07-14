using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAPI.Model.Model
{
    public class ManagerDropdownModel
    {
        public int EmployeePositionId { get; set; }
        public int EmployeeId { get; set; }

        public string FullName { get; set; }

        public string PositionName { get; set; }

        public string DepartmentName { get; set; }
        public string Email { get; set; }
    }
}
