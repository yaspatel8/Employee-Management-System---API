using EmployeeAPI.Data.DBRepositry.Department;
using EmployeeAPI.Data.DBRepositry.Employee;
using EmployeeAPI.Data.DBRepositry.Login;
using EmployeeAPI.Data.DBRepositry.Profile;
using EmployeeAPI.Data.DBRepositry.Register;
using EmployeeAPI.Data.DBRepositry.Role;
using EmployeeAPI.Data.DBRepositry.Position;

namespace EmployeeAPI.Data
{
    public static class DataRegister
    {
        public static Dictionary<Type, Type> GetTypes()
        {
            var dataDictionary = new Dictionary<Type, Type>
            {
                { typeof(IRoleRepositry), typeof(RoleRepositry) },
                { typeof(IEmployeeRepositry), typeof(EmployeeRepositry) },
                { typeof(IDepartmentRepositry), typeof(DepartmentRepositry) },
                {typeof(IRegisterRepogitry), typeof(RegisterRepogitry)  },
                {typeof(ILoginRepogistry), typeof(LoginRepositry) },
                {typeof(IProfileRepository), typeof(ProfileRepository) },
                {typeof(IPositionRepositry), typeof(Positionrepositry) },
            };
            return dataDictionary;
        }
    }
}
