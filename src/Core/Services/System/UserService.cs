using jotfi.Jot.Base.System;
using jotfi.Jot.Base.Utils;
using jotfi.Jot.Core.Services.Base;
using jotfi.Jot.Model.Base;
using jotfi.Jot.Model.System;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace jotfi.Jot.Core.Services.System
{
    public partial class UserService : BaseService
    {
        public UserService(Application app, LogOpts opts = null) : base(app, opts)
        {

        }

        public User Authenticate(string username, string password)
        {
            if (AppSettings.IsClient)
            {
                return AuthenticateClient(username, password);
            }
            var user = Repository.System.User.Get(new { UserName = username });
            user.UserName.IsEqualTo(username);
            GetUserDetails(user);
            var userpass = user.Password;
            if (!PasswordUtils.VerifyPasswordHash(password, userpass.PasswordHash, userpass.PasswordSalt))
            {
                return null;
            }           
            return user;
        }

        public bool GetPasswordValid(string password)
        {
            var passwordScore = PasswordUtils.CheckStrength(password);
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
            var passwordScore = PasswordUtils.CheckStrength(password);
            var passwordInfo = $"Password Strength: {passwordScore}.";
            if (!GetPasswordValid(password))
            {
                passwordInfo += "Please add extra length and/or complexity.";
            }
            return passwordInfo;
        }

        public bool GetEmailValid(string email)
        {
            return ValidUtils.IsEmailValid(email);
        }

        public IEnumerable<User> GetUsers()
        {
            var users = Repository.System.User.GetList();
            foreach (var user in users)
            {
                GetUserDetails(user);
            }
            return users;
        }

        public bool CreateUser(User user)
        {
            try
            {
                if (AppSettings.IsClient)
                {
                    return CreateUserClient(user);
                }
                using var uow = Database.Context.Create();
                var conn = Database.Context.GetConnection();
                var userId = Repository.System.User.Insert(user, conn);
                var personId = Repository.Base.Person.Insert(user.Person, conn);
                var emailId = Repository.Base.Email.Insert(user.Person.Email, conn);
                var addressId = Repository.Base.Address.Insert(user.Person.Address, conn);
                PasswordUtils.CreatePasswordHash(user.Password.CreatePassword, out byte[] hash, out byte[] salt);
                user.Password.PasswordHash = hash;
                user.Password.PasswordSalt = salt;
                var passwordId = Repository.Base.Password.Insert(user.Password, conn);
                AssertUpdateNewUser(userId, user.Hash, personId, passwordId, conn);
                AssertUpdateNewUserPassword(userId, passwordId, user.Password.Hash, conn);
                AssertUpdateNewUserPerson(userId, personId, emailId, addressId, user.Person.Hash, conn);
                AssertUpdateNewUserPersonEmail(personId, emailId, user.Person.Email.Hash, conn);
                AssertUpdateNewUserPersonAddress(personId, addressId, user.Person.Address.Hash, conn);
                uow.CommitAsync().Wait();
                return true;
            }
            catch (Exception ex)
            {
                Log(ex);
                return false;
            }            
        }

        void AssertUpdateNewUser(long userId, string hash, long personId, long passwordId, DbConnection conn = null)
        {
            var user = Repository.System.User.Get(userId, conn);
            user.Hash.IsEqualTo(hash);
            user.PersonId = personId;
            user.PasswordId = passwordId;
            Repository.System.User.Update(user, conn).IsEqualTo(1);
            var updatedUser = Repository.System.User.Get(userId, conn);
            updatedUser.PersonId.IsEqualTo(personId);
            updatedUser.PasswordId.IsEqualTo(passwordId);
        }

        void AssertUpdateNewUserPassword(long userId, long passwordId, string hash, DbConnection conn = null)
        {
            var password = Repository.Base.Password.Get(passwordId, conn);
            password.Hash.IsEqualTo(hash);
            password.SetTx(userId, typeof(User).Name);
            Repository.Base.Password.Update(password, conn).IsEqualTo(1);
            var updatedPassword = Repository.Base.Password.Get(passwordId, conn);
            updatedPassword.TxId.IsEqualTo(userId);
        }

        void AssertUpdateNewUserPerson(long userId, long personId, long emailId, long addressId, string hash, DbConnection conn = null)
        {
            var person = Repository.Base.Person.Get(personId, conn);
            person.Hash.IsEqualTo(hash);
            person.EmailId = emailId;
            person.AddressId = addressId;
            person.SetTx(userId, typeof(User).Name);
            Repository.Base.Person.Update(person, conn).IsEqualTo(1);
            var updatedPerson = Repository.Base.Person.Get(personId, conn);
            updatedPerson.TxId.IsEqualTo(userId);
        }

        void AssertUpdateNewUserPersonEmail(long personId, long emailId, string hash, DbConnection conn = null)
        {
            var email = Repository.Base.Email.Get(emailId, conn);
            email.Hash.IsEqualTo(hash);
            email.SetTx(personId, typeof(Person).Name);
            Repository.Base.Email.Update(email, conn).IsEqualTo(1);
            var updatedEmail = Repository.Base.Person.Get(emailId, conn);
            updatedEmail.TxId.IsEqualTo(personId);
        }

        void AssertUpdateNewUserPersonAddress(long personId, long addressId, string hash, DbConnection conn = null)
        {
            var address = Repository.Base.Address.Get(addressId, conn);
            address.Hash.IsEqualTo(hash);
            address.SetTx(personId, typeof(Person).Name);
            Repository.Base.Address.Update(address, conn).IsEqualTo(1);
            var updatedAddress = Repository.Base.Person.Get(addressId, conn);
            updatedAddress.TxId.IsEqualTo(personId);
        }

        void GetUserDetails(User user, DbConnection conn = null)
        {
            user.Password = Repository.Base.Password.Get(user.PasswordId, conn);
            user.Password.TxId.IsEqualTo(user.Id);
            user.Person = Repository.Base.Person.Get(user.PersonId, conn);
            user.Person.TxId.IsEqualTo(user.Id);
            user.Person.Email = Repository.Base.Email.Get(user.Person.EmailId, conn);
            user.Person.Email.TxId.IsEqualTo(user.Person.Id);
            user.Person.Address = Repository.Base.Address.Get(user.Person.AddressId, conn);
            user.Person.Address.TxId.IsEqualTo(user.Person.Id);
        }
    }
}
