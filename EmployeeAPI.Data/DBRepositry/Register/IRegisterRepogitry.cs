using EmployeeAPI.Common;
using EmployeeAPI.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAPI.Data.DBRepositry.Register
{
    public interface IRegisterRepogitry
    {
        Task<DbResponseModel> SaveRegister(UserModel model);
    }
}
