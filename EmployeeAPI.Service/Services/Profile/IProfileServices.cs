using EmployeeAPI.Common;
using EmployeeAPI.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAPI.Service.Services.Profile
{
    public interface IProfileServices
    {
        Task<EmployeeWithDepartmentModel> GetProfile(int id);
    }
}
