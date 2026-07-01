using EmployeeAPI.Model.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using EmployeeAPI.Common.Helper;
using EmployeeAPI.Model.Model;

namespace EmployeeAPI.Data.DBRepositry.Role
{
    public class RoleRepositry : IRoleRepositry
    {
        private readonly IDbConnection _db;

        public RoleRepositry(IDbConnection db)
        {
            _db = db;
        }

        public async Task<int> AddRole(RolesModel roles)
        {
            try
            {
                DynamicParameters param = new();
                param.Add("@RoleName", roles.RoleName);
                var result = await _db.ExecuteAsync(StoredProcedure.AddRole, param, commandType: CommandType.StoredProcedure);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<RolesModel>> GetAllRoles()
        {
            try
            {
                var result = await _db.QueryAsync<RolesModel>(StoredProcedure.GetAllRoles, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
