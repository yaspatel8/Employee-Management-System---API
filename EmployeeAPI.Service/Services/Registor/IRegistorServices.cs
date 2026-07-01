using EmployeeAPI.Common;
using EmployeeAPI.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAPI.Service.Services.Registor
{
    public interface IRegistorServices
    {
        Task<DbResponseModel> SaveRegister(UserModel model);
    }
}
