using Dapper;
using EmployeeAPI.Common.Helper;
using EmployeeAPI.Model.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAPI.Data.DBRepositry.Login
{
    public class LoginRepositry : ILoginRepogistry
    {
        private readonly IDbConnection _db;

        public LoginRepositry(IDbConnection db)
        {
            _db = db;
        }

        public async Task<LoginModel> LoginUser(LoginModel model)
        {
            try
            {
                DynamicParameters param = new();
                param.Add("@Email", model.Email);
                var result = await _db.QueryFirstOrDefaultAsync<LoginModel>(StoredProcedure.Login, param, commandType: CommandType.StoredProcedure);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
