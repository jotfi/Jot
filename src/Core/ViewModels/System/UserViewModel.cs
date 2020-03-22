using jotfi.Jot.Base.System;
using jotfi.Jot.Base.Utils;
using jotfi.Jot.Core.ViewModels.Base;
using jotfi.Jot.Core.Views.Base;
using jotfi.Jot.Model.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace jotfi.Jot.Core.ViewModels.System
{
    public class UserViewModel : BaseViewModel
    {
        public UserViewModel(Application app, LogOpts opts = null) : base(app, opts)
        {

        }


        public bool GetPasswordValid(string password)
        {
            var passwordScore = PasswordAdvisor.CheckStrength(password);
            return passwordScore switch
            {
                PasswordScore.Blank => false,
                PasswordScore.VeryWeak => false,
                PasswordScore.Weak => false,
                _ => true
            };
        }

        public string GetPasswordInfo(string password)
        {
            var passwordScore = PasswordAdvisor.CheckStrength(password);
            var passwordInfo = $"Password Strength: {passwordScore}.";
            if (!GetPasswordValid(password))
            {
                passwordInfo += "Please add extra length and/or complexity.";
            }
            return passwordInfo;
        }

        public bool GetEmailValid(string email)
        {
            return EmailValidator.IsEmailValid(email);
        }

        public bool CreateUser(User user)
        {
            using var uow = GetDatabase().Context.Create();
            var conn = GetDatabase().Context.GetConnection();
            try
            {
                var addressId = GetRepository().Base.Address.Insert(conn, user.Person.Address);
                var emailId = GetRepository().Base.Email.Insert(conn, user.Person.Email);
                user.Person.AddressId = addressId;
                user.Person.EmailId = emailId;
                var personId = GetRepository().Base.Person.Insert(conn, user.Person);
                user.PersonId = personId;
                user.Password.PasswordHash = GetPasswordHash(user.Password.CreatePassword);
                var passwordId = GetRepository().Base.Password.Insert(conn, user.Password);
                user.PasswordId = passwordId;
                var userId = GetRepository().System.User.Insert(conn, user);
            }
            catch (Exception ex)
            {
                Log(ex);
                return false;
            }
            uow.CommitAsync().Wait();
            return true;
        }

        public string GetPasswordHash(string password)
        {
            byte[] passwordBytes = Encoding.Default.GetBytes(password);
            using var SH256Password = SHA256.Create();
            byte[] hashValue = SH256Password.ComputeHash(passwordBytes);
            return Convert.ToBase64String(hashValue);
        }
    }
}
