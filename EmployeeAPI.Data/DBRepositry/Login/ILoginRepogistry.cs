using EmployeeAPI.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAPI.Data.DBRepositry.Login
{
    public interface ILoginRepogistry
    {
        Task<LoginModel> LoginUser(LoginModel model);
    }
}
