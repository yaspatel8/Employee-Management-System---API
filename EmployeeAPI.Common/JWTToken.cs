using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;


namespace EmployeeAPI.Common
{
    public static class JWTToken
    {
        public static string GenerateToken(int userId,string fullName,string email,string role, string secretKey)
        {
            var claims = new[]
            {
                new Claim( "UserId", userId.ToString()),

                new Claim( "FullName", fullName),

                new Claim( "Email", email),

                new Claim( "Role", role)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    secretKey));

            var credentials =
                new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken( issuer:"EmployeeAPI",
                audience:
                    "EmployeeUsers",

                claims: claims,

                expires:
                    DateTime.UtcNow.AddMinutes(30),

                signingCredentials:
                    credentials);

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}