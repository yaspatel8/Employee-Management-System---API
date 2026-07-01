using Dapper;
using EmployeeAPI.Common;
using EmployeeAPI.Common.Helper;
using EmployeeAPI.Model.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAPI.Data.DBRepositry.Profile
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly IDbConnection _db;
        public ProfileRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<EmployeeWithDepartmentModel> GetProfile(int id)
        {
            try
            {
                DynamicParameters param=new();
                param.Add("@UserId", id);
                var result = await _db.QueryFirstOrDefaultAsync<EmployeeWithDepartmentModel>(StoredProcedure.GetProfile, param, commandType: CommandType.StoredProcedure);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
}
        }
    }
}
