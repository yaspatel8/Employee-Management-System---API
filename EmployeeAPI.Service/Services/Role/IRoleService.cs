using EmployeeAPI.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAPI.Service.Services.Role
{
    public interface IRoleService
    {
        Task<int> AddRole(RolesModel roles);

        Task<List<RolesModel>> GetAllRoles();
    }
}
