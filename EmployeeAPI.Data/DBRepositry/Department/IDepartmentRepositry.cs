using EmployeeAPI.Common;
using EmployeeAPI.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAPI.Data.DBRepositry.Department
{
    public interface IDepartmentRepositry
    {
        Task<List<DepartmentModel>> GetAllDepartment(CommonPaginationModel model);
        //Task<DepartmentModel?> GetDepartmentById(int id);
        Task<DbResponseModel> DeleteDepartment(int id);
        Task<DbResponseModel> SaveDepartment(DepartmentModel department);
        Task<List<DepartmentModel>> GetDepartment();
    }
}
