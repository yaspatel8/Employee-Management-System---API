using EmployeeAPI.Common;
using EmployeeAPI.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAPI.Data.DBRepositry.Employee
{
    public interface IEmployeeRepositry
    {
        Task<DbResponseModel> SaveEmployee(EmployeeModel employee);

        //Task<int> AddEmployee(EmployeeModel employee);
        Task<List<EmployeeModel>> GetAllEmployees(CommonPaginationModel model);
        //Task<EmployeeModel?> GetEmployeeById(int id);
        //Task<int> UpdateEmployee(EmployeeModel employee);
        Task<DbResponseModel> DeleteEmployee(int id);
        Task<List<EmployeeWithDepartmentModel>> GetEmployeesWithDepartment();
    }
}
