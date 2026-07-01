using EmployeeAPI.Common;
using EmployeeAPI.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAPI.Service.Services.Department
{
    public interface IDepartmentService
    {
        Task<DbResponseModel> SaveDepartment(DepartmentModel department);
        Task<List<DepartmentModel>> GetAllDepartment(CommonPaginationModel model);
        //Task<DepartmentModel?> GetDepartmentById(int id);
        Task<DbResponseModel> DeleteDepartment(int id);

        Task<List<DepartmentModel>> GetDepartment();
    }   
}
