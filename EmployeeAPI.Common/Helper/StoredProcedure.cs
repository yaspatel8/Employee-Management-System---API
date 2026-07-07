using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAPI.Common.Helper
{
    public static class StoredProcedure
    {
        #region Roles
        public const string AddRole = "SP_Role_Insert";
        public const string GetAllRoles = "SP_Roles_GetAll";
        #endregion

        #region Employee
        //public const string AddEmployee = "SP_Employee_Insert";
        public const string GetAllEmployees = "SP_Employee_GetAll";
        //public const string GetEmployeeById = "SP_Employee_Get";
        //public const string UpdateEmployee = "SP_Employee_Update";
        public const string DeleteEmployee = "SP_Employee_Delete";
        public const string GetEmployeesWithDepartment = "SP_Employee_EmployeeWithDepartment";
        public const string SaveEmployee = "SP_Employee_Save";
        public const string BulkSaveEmployees = "SP_BulkSaveEmployees";
        #endregion

        #region Department

        public const string GetAllDepartment = "SP_Department_GetAll";
        //public const string GetDepartmentById = "SP_Department_Get";
        public const string DeleteDepartment = "SP_Department_Delete";
        public const string SaveDepartment = "SP_Department_Save";
        public const string SP_Department_Get = "SP_Department_Get";

        #endregion

        #region Register/Login
        public const string RegisterUser = "SP_Register";
        public const string Login = "SP_Login";
        public const string ForgotPassword = "SP_ForgotPassword";
        public const string ResetPassword = "SP_ResetPassword";
        #endregion

        #region Profile
        public const string GetProfile = "SP_Profile_Get";
        public const string UpdateProfile = "SP_Profile_Update";
        #endregion

    }
}
