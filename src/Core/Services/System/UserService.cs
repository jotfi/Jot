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
                return AuthenticateClient(username, password).Result;
            }
            var user = GetUserByName(username);
            if (!PasswordUtils.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
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
            return users;
        }

        public User GetUserById(long id, DbConnection conn = null)
        {
            if (AppSettings.IsClient)
            {
                return GetUserByIdClient(id).Result;
            }
            var user = Repository.System.User.Get(id, conn);
            user.IsNotNull();
            //GetUserDetails(user, conn);
            return user;
        }

        public User GetUserByName(string name, DbConnection conn = null)
        {
            if (AppSettings.IsClient)
            {
                return GetUserByNameClient(name).Result;
            }
            var user = Repository.System.User.Get(new { UserName = name }, conn);
            user.IsNotNull();
            //GetUserDetails(user, conn);
            return user;
        }

        public long CreateUser(User user)
        {
            try
            {
                if (AppSettings.IsClient)
                {
                    return CreateUserClient(user).Result;
                }
                using var uow = Database.Context.Create();
                var conn = Database.Context.GetConnection();
                var userId = Repository.System.User.Insert(user, conn);
                var personId = Repository.Base.Person.Insert(user.Person, conn);
                var emailId = Repository.Base.ContactDetails.Insert(user.Person.ContactDetails, conn);
                var addressId = Repository.Base.Address.Insert(user.Person.Address, conn);
                PasswordUtils.CreatePasswordHash(user.CreatePassword, out byte[] hash, out byte[] salt);
                user.PasswordHash = hash;
                user.PasswordSalt = salt;
                uow.CommitAsync().Wait();
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
