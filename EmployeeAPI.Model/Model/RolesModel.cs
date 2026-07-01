using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAPI.Model.Model
{
    public class RolesModel : CommonModel
    {  
        public int RoleId { get; set; }
        [Required]
        public string RoleName { get; set; }
    }
}
