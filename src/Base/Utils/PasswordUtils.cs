using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace jotfi.Jot.Base.Utils
{
    public enum PasswordScore
    {
        Blank = 0,
        VeryWeak = 1,
        Weak = 2,
        Medium = 3,
        Strong = 4,
        VeryStrong = 5
    }

    public class PasswordUtils
    {
        /// <summary>
        /// Checks password length and contains: a number, both lower and upper case, a symbol
        /// http://social.msdn.microsoft.com/Forums/is/csharpgeneral/thread/5e3f27d2-49af-410a-85a2-3c47e3f77fb1
        /// </summary>
        /// <param name="password"></param>
        /// <returns>PasswordScore enum value from 0 - 5</returns>
        public static PasswordScore CheckStrength(string password)
        {            
            if (password.Length < 1)
                return PasswordScore.Blank;
            if (password.Length < 4)
                return PasswordScore.VeryWeak;

            int score = 2;
            if (password.Length >= 8) 
                score++;
            if (password.Length >= 12) 
                score++;
            if (Regex.IsMatch(password, @"[0-9]+(\.[0-9][0-9]?)?"))   //number only //"^\d+$" if you need to match more than one digit.
                score++;
            if (Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z]).+$")) //both, lower and upper case
                score++;
            if (Regex.IsMatch(password, @"[!,@,#,$,%,^,&,*,?,_,~,-,£,(,)]")) //^[A-Z]+$
                score++;
            return (PasswordScore)score;
        }


        /// <summary>
        /// https://jasonwatmore.com/post/2019/10/14/aspnet-core-3-simple-api-for-authentication-registration-and-user-management
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <param name="passwordSalt"></param>
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        /// <summary>
        /// https://jasonwatmore.com/post/2019/10/14/aspnet-core-3-simple-api-for-authentication-registration-and-user-management
        /// </summary>
        /// <param name="password"></param>
        /// <param name="storedHash"></param>
        /// <param name="storedSalt"></param>
        /// <returns></returns>
        public static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");
            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }
            return true;
        }
    }
}
