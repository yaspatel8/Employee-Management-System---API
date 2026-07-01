using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAPI.Model.Model
{
    public class DepartmentModel : CommonModel
    {
        public int DepartmentId { get; set; }
        [Required]
        public string DepartmentName { get; set; }
    }

}
