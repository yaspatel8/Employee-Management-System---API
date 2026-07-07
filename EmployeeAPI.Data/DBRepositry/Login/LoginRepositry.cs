using Dapper;
using EmployeeAPI.Common;
using EmployeeAPI.Common.Helper;
using EmployeeAPI.Model;
using EmployeeAPI.Model.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace EmployeeAPI.Data.DBRepositry.Login
{
    public class LoginRepositry : ILoginRepogistry
    {
        private readonly IDbConnection _db;
        private readonly EmailSettings _emailSettings;

        public LoginRepositry(IDbConnection db, IOptions<EmailSettings> emailSettings)
        {
            _db = db;
            _emailSettings = emailSettings.Value;
        }

        public async Task<LoginModel> LoginUser(LoginModel model)
        {
           
                DynamicParameters param = new();
                param.Add("@Email", model.Email);
                var result = await _db.QueryFirstOrDefaultAsync<LoginModel>(StoredProcedure.Login, param, commandType: CommandType.StoredProcedure);
                return result;
          
        }
        public async Task<ApiResponseModel> ForgotPassword(string email, string token)
        {
           
            DynamicParameters param = new();
            param.Add("@Email", email);
            param.Add("@Token", token);
            var result = await _db.QueryFirstOrDefaultAsync<ApiResponseModel>(StoredProcedure.ForgotPassword, param, commandType: CommandType.StoredProcedure);

            // Email service call

            return result;
        }
        public async Task SendEmailAsync(string toEmail, string subject, string resetLink)
        {
            //string body = await File.ReadAllTextAsync( Path.Combine(AppContext.BaseDirectory, "EmailTemplates", "ForgetPassword.html")); 
            string body = await File.ReadAllTextAsync("EmailTemplates/ForgotPassword.html");

            body = body.Replace("{{Email}}", toEmail);
            body = body.Replace("{{ResetLink}}", resetLink);

            using MailMessage message = new();
          
            message.From = new MailAddress(_emailSettings.FromEmail, _emailSettings.DisplayName);

            message.To.Add(toEmail);

            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            // Create HTML View
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");
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
        public async Task<ApiResponseModel> ResetPassword(resetPasswordModel model)
        {
            DynamicParameters param = new();
            param.Add("@Token", model.Token);
            param.Add("@PasswordHash", model.NewPassword);
            var result = await _db.QueryFirstOrDefaultAsync<ApiResponseModel>(StoredProcedure.ResetPassword, param, commandType: CommandType.StoredProcedure);
            return result;
        }
    }
}
