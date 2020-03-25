using jotfi.Jot.Base.System;
using jotfi.Jot.Base.Utils;
using jotfi.Jot.Core.ViewModels.Base;
using jotfi.Jot.Model.Base;
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
            return Validators.IsEmailValid(email);
        }

        public bool CreateUser(User user)
        {
            try
            {
                using var uow = GetDatabase().Context.Create();
                var conn = GetDatabase().Context.GetConnection();
                var userId = GetRepository().System.User.Insert(user, conn);
                var personId = GetRepository().Base.Person.Insert(user.Person, conn);
                var emailId = GetRepository().Base.Email.Insert(user.Person.Email, conn);
                var addressId = GetRepository().Base.Address.Insert(user.Person.Address, conn);
                user.Password.PasswordHash = HashUtils.GetSHA256Hash(user.Password.CreatePassword);
                var passwordId = GetRepository().Base.Password.Insert(user.Password, conn);
                AssertUpdateNewUser(userId, user.Hash, personId, passwordId, conn);
                AssertUpdateNewUserPassword(userId, passwordId, user.Password.Hash, conn);
                AssertUpdateNewUserPerson(userId, personId, user.Person.Hash, conn);
                AssertUpdateNewUserPersonEmail(personId, emailId, user.Person.Email.Hash, conn);
                AssertUpdateNewUserPersonAddress(personId, addressId, user.Person.Address.Hash, conn);
                uow.CommitAsync().Wait();
            }
            catch (Exception ex)
            {
                Log(ex);
                return false;
            }
            return true;
        }


        void AssertUpdateNewUser(long userId, string hash, long personId, long passwordId, DbConnection conn = null)
        {
            var user = GetRepository().System.User.GetById(userId, conn);
            user.Hash.IsEqualTo(hash);
            user.PersonId = personId;
            user.PasswordId = passwordId;
            GetRepository().System.User.Update(user, conn).IsEqualTo(1);
            var updatedUser = GetRepository().System.User.GetById(userId, conn);
            updatedUser.PersonId.IsEqualTo(personId);
            updatedUser.PasswordId.IsEqualTo(passwordId);
        }

        void AssertUpdateNewUserPassword(long userId, long passwordId, string hash, DbConnection conn = null)
        {
            var password = GetRepository().Base.Password.GetById(passwordId, conn);
            password.Hash.IsEqualTo(hash);
            password.SetTx(userId, typeof(User).Name);
            GetRepository().Base.Password.Update(password, conn).IsEqualTo(1);
            var updatedPassword = GetRepository().Base.Password.GetById(passwordId, conn);
            updatedPassword.TxId.IsEqualTo(userId);
        }

        void AssertUpdateNewUserPerson(long userId, long personId, string hash, DbConnection conn = null)
        {
            var person = GetRepository().Base.Person.GetById(personId, conn);
            person.Hash.IsEqualTo(hash);
            person.SetTx(userId, typeof(User).Name);
            GetRepository().Base.Person.Update(person, conn).IsEqualTo(1);
            var updatedPerson = GetRepository().Base.Person.GetById(personId, conn);
            updatedPerson.TxId.IsEqualTo(userId);
        }

        void AssertUpdateNewUserPersonEmail(long personId, long emailId, string hash, DbConnection conn = null)
        {
            var email = GetRepository().Base.Email.GetById(emailId, conn);
            email.Hash.IsEqualTo(hash);
            email.SetTx(personId, typeof(Person).Name);
            GetRepository().Base.Email.Update(email, conn).IsEqualTo(1);
            var updatedEmail = GetRepository().Base.Person.GetById(emailId, conn);
            updatedEmail.TxId.IsEqualTo(personId);
        }

        void AssertUpdateNewUserPersonAddress(long personId, long addressId, string hash, DbConnection conn = null)
        {
            var address = GetRepository().Base.Address.GetById(addressId, conn);
            address.Hash.IsEqualTo(hash);
            address.SetTx(personId, typeof(Person).Name);
            GetRepository().Base.Address.Update(address, conn).IsEqualTo(1);
            var updatedAddress = GetRepository().Base.Person.GetById(addressId, conn);
            updatedAddress.TxId.IsEqualTo(personId);
        }

        void GetUserDetails(User user, DbConnection conn = null)
        {
            user.Password = GetRepository().Base.Password.GetById(user.PasswordId, conn);
            user.Password.TxId.IsEqualTo(user.Id);
            user.Person = GetRepository().Base.Person.GetById(user.PersonId, conn);
            user.Person.TxId.IsEqualTo(user.Id);
            user.Person.Email = GetRepository().Base.Email.GetById(user.Person.EmailId, conn);
            user.Person.Email.TxId.IsEqualTo(user.Person.Id);
            user.Person.Address = GetRepository().Base.Address.GetById(user.Person.AddressId, conn);
            user.Person.Address.TxId.IsEqualTo(user.Person.Id);
        }
    }
}
