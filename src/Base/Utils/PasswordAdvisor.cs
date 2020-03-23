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

    public class PasswordAdvisor
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
    }
}
