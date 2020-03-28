// Copyright 2020 John Cottrell
//
// This file is part of Jot.
//
// Jot is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Jot is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with Jot.  If not, see <https://www.gnu.org/licenses/>.

using jotfi.Jot.Base.System;
using jotfi.Jot.Base.Utils;
using jotfi.Jot.Model.Base;
using jotfi.Jot.Model.System;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace jotfi.Jot.Core.Services.System
{
    public partial class UserService
    {
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            var users = await Repository.System.User.GetListAsync();
            return users;
        }

        public async Task<User> GetUserAsync(object id)
        {
            var user = await Repository.System.User.GetAsync(id);
            user.IsNotNull();
            return user;
        }

        public async Task<User> GetUserByIdAsync(long id)
        {
            var user = await Repository.System.User.GetAsync(id);
            user.IsNotNull();
            return user;
        }

        public async Task<User> GetUserByNameAsync(string name)
        {
            var user = await Repository.System.User.GetAsync(new { UserName = name });
            user.IsNotNull();
            return user;
        }

        public async Task<long> CreateUserAsync(User user)
        {
            try
            {
                using var uow = Database.Context.Create();
                var conn = Database.Context.GetConnection();
                var userId = await Repository.System.User.InsertAsync(user, conn);
                var personId = await Repository.Base.Person.InsertAsync(user.Person, conn);
                var emailId = await Repository.Base.ContactDetails.InsertAsync(user.Person.ContactDetails, conn);
                var addressId = await Repository.Base.Address.InsertAsync(user.Person.Address, conn);
                PasswordUtils.CreatePasswordHash(user.CreatePassword, out byte[] hash, out byte[] salt);
                user.PasswordHash = hash;
                user.PasswordSalt = salt;            
                await uow.CommitAsync();
                return userId;
            }
            catch (Exception ex)
            {
                Log(ex);
                return 0;
            }
        }

    }
}
