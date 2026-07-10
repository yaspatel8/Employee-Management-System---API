using EmployeeAPI.Common.Export;
using EmployeeAPI.Service.Services.Department;
using EmployeeAPI.Service.Services.Employee;
using EmployeeAPI.Service.Services.Login;
using EmployeeAPI.Service.Services.Position;
using EmployeeAPI.Service.Services.Profile;
using EmployeeAPI.Service.Services.Registor;
using EmployeeAPI.Service.Services.Role;

namespace EmployeeAPI.Service
{
    public class ServiceRegister
    {
        public static Dictionary<Type, Type> GetTypes()
        {
            var serviceDictonary = new Dictionary<Type, Type>
            {
                { typeof(IDepartmentService), typeof(DepartmentService) },
                { typeof(IEmployeeService), typeof(EmployeeService) },
                { typeof(IRoleService), typeof(RoleService) },
                { typeof(IRegistorServices), typeof(RegistorServices) },
                { typeof(ILoginServices), typeof(Loginservices) },
                {typeof(IProfileServices), typeof(ProfileService) },
                {typeof(IExcelExportService), typeof(ExcelExportService) },
                {typeof(IPositionServices), typeof(PositionServices) },

            };
            return serviceDictonary;
        }
    }
}
