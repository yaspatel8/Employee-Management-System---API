using EmployeeAPI.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAPI.Data.DBRepositry.Role
{
    public interface IRoleRepositry
    {
        Task<int> AddRole(RolesModel roles);

        Task<List<RolesModel>> GetAllRoles();
    }
}
