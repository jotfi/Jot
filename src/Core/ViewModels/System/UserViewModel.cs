using jotfi.Jot.Base.System;
using jotfi.Jot.Base.Utils;
using jotfi.Jot.Core.ViewModels.Base;
using jotfi.Jot.Model.Base;
using jotfi.Jot.Model.System;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace jotfi.Jot.Core.ViewModels.System
{
    public partial class UserViewModel : BaseViewModel
    {
        public UserViewModel(Application app, LogOpts opts = null) : base(app, opts)
        {

        }

        public User Authenticate(string username, string password)
        {
            if (GetAppSettings().IsClient)
            {
                return AuthenticateClient(username, password);
            }
            var user = GetRepository().System.User.Get(new { UserName = username });
            user.UserName.IsEqualTo(username);
            GetUserDetails(user);
            var userpass = user.Password;
            if (!PasswordUtils.VerifyPasswordHash(password, userpass.PasswordHash, userpass.PasswordSalt))
            {
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(GetApp().AppSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim("hash", user.Hash)
                    //new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
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
            var users = GetRepository().System.User.GetList();
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
                if (GetAppSettings().IsClient)
                {
                    return CreateUserClient(user);
                }
                using var uow = GetDatabase().Context.Create();
                var conn = GetDatabase().Context.GetConnection();
                var userId = GetRepository().System.User.Insert(user, conn);
                var personId = GetRepository().Base.Person.Insert(user.Person, conn);
                var emailId = GetRepository().Base.Email.Insert(user.Person.Email, conn);
                var addressId = GetRepository().Base.Address.Insert(user.Person.Address, conn);
                PasswordUtils.CreatePasswordHash(user.Password.CreatePassword, out byte[] hash, out byte[] salt);
                user.Password.PasswordHash = hash;
                user.Password.PasswordSalt = salt;
                var passwordId = GetRepository().Base.Password.Insert(user.Password, conn);
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
            var user = GetRepository().System.User.Get(userId, conn);
            user.Hash.IsEqualTo(hash);
            user.PersonId = personId;
            user.PasswordId = passwordId;
            GetRepository().System.User.Update(user, conn).IsEqualTo(1);
            var updatedUser = GetRepository().System.User.Get(userId, conn);
            updatedUser.PersonId.IsEqualTo(personId);
            updatedUser.PasswordId.IsEqualTo(passwordId);
        }

        void AssertUpdateNewUserPassword(long userId, long passwordId, string hash, DbConnection conn = null)
        {
            var password = GetRepository().Base.Password.Get(passwordId, conn);
            password.Hash.IsEqualTo(hash);
            password.SetTx(userId, typeof(User).Name);
            GetRepository().Base.Password.Update(password, conn).IsEqualTo(1);
            var updatedPassword = GetRepository().Base.Password.Get(passwordId, conn);
            updatedPassword.TxId.IsEqualTo(userId);
        }

        void AssertUpdateNewUserPerson(long userId, long personId, long emailId, long addressId, string hash, DbConnection conn = null)
        {
            var person = GetRepository().Base.Person.Get(personId, conn);
            person.Hash.IsEqualTo(hash);
            person.EmailId = emailId;
            person.AddressId = addressId;
            person.SetTx(userId, typeof(User).Name);
            GetRepository().Base.Person.Update(person, conn).IsEqualTo(1);
            var updatedPerson = GetRepository().Base.Person.Get(personId, conn);
            updatedPerson.TxId.IsEqualTo(userId);
        }

        void AssertUpdateNewUserPersonEmail(long personId, long emailId, string hash, DbConnection conn = null)
        {
            var email = GetRepository().Base.Email.Get(emailId, conn);
            email.Hash.IsEqualTo(hash);
            email.SetTx(personId, typeof(Person).Name);
            GetRepository().Base.Email.Update(email, conn).IsEqualTo(1);
            var updatedEmail = GetRepository().Base.Person.Get(emailId, conn);
            updatedEmail.TxId.IsEqualTo(personId);
        }

        void AssertUpdateNewUserPersonAddress(long personId, long addressId, string hash, DbConnection conn = null)
        {
            var address = GetRepository().Base.Address.Get(addressId, conn);
            address.Hash.IsEqualTo(hash);
            address.SetTx(personId, typeof(Person).Name);
            GetRepository().Base.Address.Update(address, conn).IsEqualTo(1);
            var updatedAddress = GetRepository().Base.Person.Get(addressId, conn);
            updatedAddress.TxId.IsEqualTo(personId);
        }

        void GetUserDetails(User user, DbConnection conn = null)
        {
            user.Password = GetRepository().Base.Password.Get(user.PasswordId, conn);
            user.Password.TxId.IsEqualTo(user.Id);
            user.Person = GetRepository().Base.Person.Get(user.PersonId, conn);
            user.Person.TxId.IsEqualTo(user.Id);
            user.Person.Email = GetRepository().Base.Email.Get(user.Person.EmailId, conn);
            user.Person.Email.TxId.IsEqualTo(user.Person.Id);
            user.Person.Address = GetRepository().Base.Address.Get(user.Person.AddressId, conn);
            user.Person.Address.TxId.IsEqualTo(user.Person.Id);
        }
    }
}
