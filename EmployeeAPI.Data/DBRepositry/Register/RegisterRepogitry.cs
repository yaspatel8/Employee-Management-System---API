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

namespace EmployeeAPI.Data.DBRepositry.Register
{
    public class RegisterRepogitry : IRegisterRepogitry
    {
        private readonly IDbConnection _db;

        public RegisterRepogitry(IDbConnection db)
        {
            _db = db;
        }
        public async Task<DbResponseModel> SaveRegister(UserModel model)
        {
            try
            {
                DynamicParameters param = new();
                param.Add("@FullName", model.FullName);
                param.Add("@Email", model.Email);
                param.Add("@PasswordHash", model.PasswordHash);
                var result = await _db.QueryFirstOrDefaultAsync<DbResponseModel>(StoredProcedure.RegisterUser, param, commandType: CommandType.StoredProcedure);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
       
}
