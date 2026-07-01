using Dapper;
using EmployeeAPI.Data.DBRepositry.Role;
using EmployeeAPI.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAPI.Service.Services.Role
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepositry _repositry;
        public RoleService(IRoleRepositry repositry)
        {
            _repositry = repositry;
        }
        public async Task<int> AddRole(RolesModel roles)
        {
            return await _repositry.AddRole(roles);
        }
        public async Task<List<RolesModel>> GetAllRoles()
        {
            return await _repositry.GetAllRoles();
        }
    }
}