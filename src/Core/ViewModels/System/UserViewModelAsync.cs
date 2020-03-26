using jotfi.Jot.Base.System;
using jotfi.Jot.Base.Utils;
using jotfi.Jot.Model.Base;
using jotfi.Jot.Model.System;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace jotfi.Jot.Core.ViewModels.System
{
    public partial class UserViewModel
    {
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            var users = await Repository.System.User.GetListAsync();
            foreach (var user in users)
            {
                await GetUserDetailsAsync(user);
            }
            return users;
        }

        public async Task<User> GetUserAsync(long id) => await Repository.System.User.GetByIdAsync(id);

        public async Task<bool> CreateUserAsync(User user)
        {
            try
            {
                using var uow = Database.Context.Create();
                var conn = Database.Context.GetConnection();
                var userId = await Repository.System.User.InsertAsync(user, conn);
                var personId = await Repository.Base.Person.InsertAsync(user.Person, conn);
                var emailId = await Repository.Base.Email.InsertAsync(user.Person.Email, conn);
                var addressId = await Repository.Base.Address.InsertAsync(user.Person.Address, conn);
                PasswordUtils.CreatePasswordHash(user.Password.CreatePassword, out byte[] hash, out byte[] salt);
                user.Password.PasswordHash = hash;
                user.Password.PasswordSalt = salt;
                var passwordId = await Repository.Base.Password.InsertAsync(user.Password, conn);
                await AssertUpdateNewUserAsync(userId, user.Hash, personId, passwordId, conn);
                await AssertUpdateNewUserPasswordAsync(userId, passwordId, user.Password.Hash, conn);
                await AssertUpdateNewUserPersonAsync(userId, personId, emailId, addressId, user.Person.Hash, conn);
                await AssertUpdateNewUserPersonEmailAsync(personId, emailId, user.Person.Email.Hash, conn);
                await AssertUpdateNewUserPersonAddressAsync(personId, addressId, user.Person.Address.Hash, conn);
                await uow.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                Log(ex);
            }
            return false;
        }

        async Task AssertUpdateNewUserAsync(long userId, string hash, long personId, long passwordId, DbConnection conn = null)
        {
            var user = await Repository.System.User.GetByIdAsync(userId, conn);
            user.Hash.IsEqualTo(hash);
            user.PersonId = personId;
            user.PasswordId = passwordId;
            var rows = await Repository.System.User.UpdateAsync(user, conn);
            rows.IsEqualTo(1);
            var updatedUser = await Repository.System.User.GetByIdAsync(userId, conn);
            updatedUser.PersonId.IsEqualTo(personId);
            updatedUser.PasswordId.IsEqualTo(passwordId);
        }

        async Task AssertUpdateNewUserPasswordAsync(long userId, long passwordId, string hash, DbConnection conn = null)
        {
            var password = await Repository.Base.Password.GetByIdAsync(passwordId, conn);
            password.Hash.IsEqualTo(hash);
            password.SetTx(userId, typeof(User).Name);
            var rows = await Repository.Base.Password.UpdateAsync(password, conn);
            rows.IsEqualTo(1);
            var updatedPassword = await Repository.Base.Password.GetByIdAsync(passwordId, conn);
            updatedPassword.TxId.IsEqualTo(userId);
        }

        async Task AssertUpdateNewUserPersonAsync(long userId, long personId, long emailId, long addressId, string hash, DbConnection conn = null)
        {
            var person = await Repository.Base.Person.GetByIdAsync(personId, conn);
            person.Hash.IsEqualTo(hash);
            person.EmailId = emailId;
            person.AddressId = addressId;
            person.SetTx(userId, typeof(User).Name);
            var rows = await Repository.Base.Person.UpdateAsync(person, conn);
            rows.IsEqualTo(1);
            var updatedPerson = await Repository.Base.Person.GetByIdAsync(personId, conn);
            updatedPerson.TxId.IsEqualTo(userId);
        }

        async Task AssertUpdateNewUserPersonEmailAsync(long personId, long emailId, string hash, DbConnection conn = null)
        {
            var email = await Repository.Base.Email.GetByIdAsync(emailId, conn);
            email.Hash.IsEqualTo(hash);
            email.SetTx(personId, typeof(Person).Name);
            var rows = await Repository.Base.Email.UpdateAsync(email, conn);
            rows.IsEqualTo(1);
            var updatedEmail = await Repository.Base.Person.GetByIdAsync(emailId, conn);
            updatedEmail.TxId.IsEqualTo(personId);
        }

        async Task AssertUpdateNewUserPersonAddressAsync(long personId, long addressId, string hash, DbConnection conn = null)
        {
            var address = await Repository.Base.Address.GetByIdAsync(addressId, conn);
            address.Hash.IsEqualTo(hash);
            address.SetTx(personId, typeof(Person).Name);
            var rows = await Repository.Base.Address.UpdateAsync(address, conn);
            rows.IsEqualTo(1);
            var updatedAddress = await Repository.Base.Person.GetByIdAsync(addressId, conn);
            updatedAddress.TxId.IsEqualTo(personId);
        }

        async Task GetUserDetailsAsync(User user, DbConnection conn = null)
        {
            user.Password = await Repository.Base.Password.GetByIdAsync(user.PasswordId, conn);
            user.Password.TxId.IsEqualTo(user.Id);
            user.Person = await Repository.Base.Person.GetByIdAsync(user.PersonId, conn);
            user.Person.TxId.IsEqualTo(user.Id);
            user.Person.Email = await Repository.Base.Email.GetByIdAsync(user.Person.EmailId, conn);
            user.Person.Email.TxId.IsEqualTo(user.Person.Id);
            user.Person.Address = await Repository.Base.Address.GetByIdAsync(user.Person.AddressId, conn);
            user.Person.Address.TxId.IsEqualTo(user.Person.Id);
        }

    }
}
