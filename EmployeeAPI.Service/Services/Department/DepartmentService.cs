using EmployeeAPI.Common;
using EmployeeAPI.Data.DBRepositry.Department;
using EmployeeAPI.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAPI.Service.Services.Department
{
    public class DepartmentService : IDepartmentService

    {
        private readonly IDepartmentRepositry _repositry;
        public DepartmentService(IDepartmentRepositry repositry)
        {
            _repositry = repositry;
        }


        public async Task<DbResponseModel> SaveDepartment (DepartmentModel department)
        {
            return await _repositry.SaveDepartment(department);
        }

        public async Task<List<DepartmentModel>> GetAllDepartment(CommonPaginationModel model)
        {
            return await _repositry.GetAllDepartment(model);
        }
        // public async Task<DepartmentModel?> GetDepartmentById(int id)
        //{
        //    return await _repositry.GetDepartmentById(id);
        //}

        public async Task<DbResponseModel> DeleteDepartment(int id)
        {
            return await _repositry.DeleteDepartment(id);
        }
        public async Task<List<DepartmentModel>> GetDepartment()
        {
            return await _repositry.GetDepartment();
        }
    }
}
