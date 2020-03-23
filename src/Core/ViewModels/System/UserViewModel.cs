using jotfi.Jot.Base.System;
using jotfi.Jot.Base.Utils;
using jotfi.Jot.Core.ViewModels.Base;
using jotfi.Jot.Model.System;
using System;
using System.Data.Common;

namespace jotfi.Jot.Core.ViewModels.System
{
    public partial class UserViewModel : BaseViewModel
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
            try
            {
                using var uow = GetDatabase().Context.Create();
                var conn = GetDatabase().Context.GetConnection();
                var addressId = GetRepository().Base.Address.Insert(user.Person.Address, conn);
                var emailId = GetRepository().Base.Email.Insert(user.Person.Email, conn);
                user.Person.AddressId = addressId;
                user.Person.EmailId = emailId;
                var personId = GetRepository().Base.Person.Insert(user.Person, conn);
                user.PersonId = personId;
                user.Password.PasswordHash = HashUtils.GetSHA256Hash(user.Password.CreatePassword);
                var passwordId = GetRepository().Base.Password.Insert(user.Password, conn);
                user.PasswordId = passwordId;
                var userId = GetRepository().System.User.Insert(user, conn);
                AssertNewUser(userId, user, conn);
                uow.CommitAsync().Wait();
            }
            catch (Exception ex)
            {
                Log(ex);
                return false;
            }
            return true;
        }

        void AssertNewUser(long userId, User user, DbConnection conn = null)
        {
            var newUser = GetRepository().System.User.GetById(userId, conn);
            newUser.UserName.IsEqualTo(user.UserName);
            var password = GetRepository().Base.Password.GetById(newUser.PasswordId, conn);
            password.PasswordHash.IsEqualTo(user.Password.PasswordHash);
            var person = GetRepository().Base.Person.GetById(newUser.PersonId, conn);
            person.Hash.IsEqualTo(user.Person.Hash);
            var email = GetRepository().Base.Email.GetById(person.EmailId, conn);
            email.EmailAddress.IsEqualTo(user.Person.Email.EmailAddress);
            var address = GetRepository().Base.Address.GetById(person.AddressId, conn);
            address.Hash.IsEqualTo(user.Person.Address.Hash);
        }
    }
}
