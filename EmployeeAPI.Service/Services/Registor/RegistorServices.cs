using EmployeeAPI.Common;
using EmployeeAPI.Data.DBRepositry.Register;
using EmployeeAPI.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAPI.Service.Services.Registor
{
    public class RegistorServices : IRegistorServices
    {
        private readonly IRegisterRepogitry _registerRepogitry;

        public RegistorServices(IRegisterRepogitry registerRepogitry)
        {
            _registerRepogitry = registerRepogitry;
        }

        public async Task<DbResponseModel> SaveRegister(UserModel model)
        {
            return await _registerRepogitry.SaveRegister(model);
        }
    }
}
