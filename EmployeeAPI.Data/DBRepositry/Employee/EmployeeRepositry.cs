using Dapper;
using EmployeeAPI.Common;
using EmployeeAPI.Common.Helper;
using EmployeeAPI.Model;
using EmployeeAPI.Model.Model;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAPI.Data.DBRepositry.Employee
{
    public class EmployeeRepositry : IEmployeeRepositry
    {
        private readonly IDbConnection _db;
        private readonly EmailSettings _emailSettings;

        public EmployeeRepositry(IDbConnection db,IOptions<EmailSettings> emailSettings)
        {
            _db = db;
            _emailSettings = emailSettings.Value;
        }

        public async Task<DbResponseModel> SaveEmployee(EmployeeModel employee)
        {

            DynamicParameters param = new();
            param.Add("@EmployeeId", employee.EmployeeId);
            param.Add("@FullName", employee.FullName);
            param.Add("@Email", employee.Email);
            param.Add("@PhoneNumber", employee.PhoneNumber);
            param.Add("@Salary", employee.Salary);
            param.Add("@DepartmentId", employee.DepartmentId);
            param.Add("@RoleId", employee.RoleId);
            param.Add("@PasswordHash", employee.PasswordHash);
            param.Add("@ProfileImage", employee.ProfileImage);
            param.Add("@OldFileName", dbType: DbType.String, direction: ParameterDirection.Output, size: 200);
            param.Add("@UpdatedBy", employee.UpdatedBy);
            param.Add("@CreatedBy", employee.CreatedBy);

            var result = await _db.QueryFirstOrDefaultAsync<DbResponseModel>(StoredProcedure.SaveEmployee, param, commandType: CommandType.StoredProcedure);

            result.OldFileName = param.Get<string>("@OldFileName");

            return result;

        }

        //public async Task<int> AddEmployee(EmployeeModel employee)
        //{
        //    try
        //    {
        //        DynamicParameters param = new();
        //        param.Add("@EmployeeName", employee.EmployeeName);
        //        param.Add("@EmployeeEmail", employee.EmployeeEmail);
        //        param.Add("@PhoneNumber", employee.PhoneNumber);
        //        param.Add("@Salary", employee.Salary);
        //        param.Add("@DepartmentId", employee.DepartmentId);
        //        var result = await _db.ExecuteAsync(StoredProcedure.AddEmployee, param, commandType: CommandType.StoredProcedure);
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        public async Task<List<EmployeeModel>> GetAllEmployees(CommonPaginationModel model)
        {

            DynamicParameters param = new();
            param.Add("@SearchText", model.SearchText);
            param.Add("@PageNumber", model.PageNumber);
            param.Add("@PageSize", model.PageSize);
            param.Add("@SortColumn", model.SortColumn);
            param.Add("@SortOrder", model.SortOrder);
            var result = await _db.QueryAsync<EmployeeModel>(StoredProcedure.GetAllEmployees, param, commandType: CommandType.StoredProcedure);
            return result.ToList();

        }
        //public async Task<EmployeeModel?> GetEmployeeById(int id)
        //{
        //    try
        //    {
        //        DynamicParameters param = new();
        //        param.Add("@EmployeeId", id);
        //        var result = await _db.QueryFirstOrDefaultAsync<EmployeeModel>(StoredProcedure.GetEmployeeById, param, commandType: CommandType.StoredProcedure);
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}
        //public async Task<int> UpdateEmployee(EmployeeModel employee)
        //{
        //    try
        //    {
        //        DynamicParameters param = new();
        //        param.Add("@EmployeeId", employee.EmployeeId);
        //        param.Add("@EmployeeName", employee.EmployeeName);
        //        param.Add("@EmployeeEmail", employee.EmployeeEmail);
        //        param.Add("@PhoneNumber", employee.PhoneNumber);
        //        param.Add("@Salary", employee.Salary);
        //        var result = await _db.ExecuteAsync(StoredProcedure.UpdateEmployee, param, commandType: CommandType.StoredProcedure);
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}
        public async Task<DbResponseModel> DeleteEmployee(int id)
        {
            DynamicParameters param = new();
            param.Add("@EmployeeId", id);
            var result = await _db.QueryFirstOrDefaultAsync<DbResponseModel>(StoredProcedure.DeleteEmployee, param, commandType: CommandType.StoredProcedure);
            return result;

        }
        public async Task<List<EmployeeWithDepartmentModel>> GetEmployeesWithDepartment()
        {

            var result = await _db.QueryAsync<EmployeeWithDepartmentModel>(StoredProcedure.GetEmployeesWithDepartment, commandType: CommandType.StoredProcedure);
            return result.ToList();

        }
        public async Task<BulkDbResponseModel> BulkSaveEmployees(List<EmployeeModel> employees)
        {
            //DataTable use for bulk insert, create a DataTable and populate it with employee data
            DataTable dt = new();
            dt.Columns.Add("FullName", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("PasswordHash", typeof(string));
            dt.Columns.Add("Salary", typeof(decimal));
            dt.Columns.Add("DepartmentId", typeof(int));

            foreach (var employee in employees)
            {
                dt.Rows.Add(employee.FullName, employee.Email, employee.PasswordHash, employee.Salary, employee.DepartmentId);
            }

            DynamicParameters param = new();
            param.Add("@Employees", dt.AsTableValuedParameter("dbo.EmployeeType")); // Assuming you have a user-defined table type named EmployeeType
            param.Add("@CreatedBy", employees.FirstOrDefault()?.CreatedBy);
            var result = await _db.QueryFirstOrDefaultAsync<BulkDbResponseModel>(StoredProcedure.BulkSaveEmployees, param, commandType: CommandType.StoredProcedure);

            return result;

        }
        public async Task SendEmployeeCreatedEmailAsync(string toEmail, string fullName, string password, string loginLink)
        {
            string body = await File.ReadAllTextAsync("EmailTemplates/Welcome.html");

            body = body.Replace("{{FullName}}", fullName);
            body = body.Replace("{{Email}}", toEmail);
            body = body.Replace("{{Password}}", password);
            body = body.Replace("{{LoginLink}}", loginLink);

            using MailMessage message = new();

            message.From = new MailAddress(
                _emailSettings.FromEmail,
                _emailSettings.DisplayName);

            message.To.Add(toEmail);

            message.Subject = "Welcome to Employee Management System";
            message.IsBodyHtml = true;

            AlternateView htmlView =
                AlternateView.CreateAlternateViewFromString(body, null, "text/html");

            LinkedResource logo = new LinkedResource("Documents/logo.jpg");
            logo.ContentId = "companylogo";

            htmlView.LinkedResources.Add(logo);

            message.AlternateViews.Add(htmlView);

            using SmtpClient smtp = new SmtpClient(
                _emailSettings.Host,
                _emailSettings.Port);

            smtp.Credentials = new NetworkCredential(
                _emailSettings.Username,
                _emailSettings.Password);

            smtp.EnableSsl = _emailSettings.EnableSsl;

            await smtp.SendMailAsync(message);
        }
    }
}
