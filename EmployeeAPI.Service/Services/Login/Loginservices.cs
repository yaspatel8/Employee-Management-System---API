using EmployeeAPI.Common;
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
        public async Task<ApiResponseModel> ForgotPassword(string email, string token)
        {
            return await _loginRepogitry.ForgotPassword(email, token);
        }
        public async Task SendEmailAsync(string toEmail, string subject, string resetLink)
        {
             await _loginRepogitry.SendEmailAsync(toEmail, subject, resetLink);
        }
    }
}
