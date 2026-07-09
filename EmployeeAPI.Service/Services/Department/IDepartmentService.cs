using EmployeeAPI.Common;
using EmployeeAPI.Model.Model;
using EmployeeAPI.Model.Model.Export;
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
        Task<ApiResponseModel> UpdateDepartmentStatus(int departmentId, bool isActive, int updatedBy);
        Task<List<DepartmentExportModel>> ExportDepartments(List<int> ids);

    }   

}
