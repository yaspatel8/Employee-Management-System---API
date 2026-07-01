using EmployeeAPI.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAPI.Service.Services.Login
{
    public interface ILoginServices
    {
        Task<LoginModel> LoginUser(LoginModel model);
    }
}
