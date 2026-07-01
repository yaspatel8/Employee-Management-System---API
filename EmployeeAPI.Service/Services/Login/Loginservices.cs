using EmployeeAPI.Data.DBRepositry.Login;
using EmployeeAPI.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAPI.Service.Services.Login
{
    public class Loginservices : ILoginServices
    {
        private readonly ILoginRepogistry _loginRepogitry;
        public Loginservices(ILoginRepogistry loginRepogitry)
        {
            _loginRepogitry = loginRepogitry;
        }
        public async Task<LoginModel> LoginUser(LoginModel model)
        {
            return await _loginRepogitry.LoginUser(model);
        }
    }
}
