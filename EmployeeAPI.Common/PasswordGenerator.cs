using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;


namespace EmployeeAPI.Common
{
    public static class PasswordGenerator
    {
        private const string Upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string Lower = "abcdefghijklmnopqrstuvwxyz";
        private const string Numbers = "0123456789";
        private const string Special = "@#$%&*!?";
        private const string All = Upper + Lower + Numbers + Special;

        public static string GeneratePassword(int length = 6)
        {
            if (length < 4)
                throw new ArgumentException("Password length must be at least 4.");

            List<char> password = new();

            // Ensure required character types
            password.Add(GetRandomChar(Upper));
            password.Add(GetRandomChar(Lower));
            password.Add(GetRandomChar(Numbers));
            password.Add(GetRandomChar(Special));

            // Fill remaining characters
            while (password.Count < length)
            {
                password.Add(GetRandomChar(All));
            }

            // Shuffle characters
            return new string(password.OrderBy(_ => GetRandomInt()).ToArray());
        }

        private static char GetRandomChar(string chars)
        {
            return chars[RandomNumberGenerator.GetInt32(chars.Length)];
        }

        private static int GetRandomInt()
        {
            return RandomNumberGenerator.GetInt32(int.MaxValue);
        }
    }
}
