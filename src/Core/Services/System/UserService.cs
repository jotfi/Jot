#region License
//
// Copyright (c) 2020, John Cottrell <me@john.co.com>
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
//
#endregion
using jotfi.Jot.Base.Settings;
using jotfi.Jot.Base.Utils;
using jotfi.Jot.Core.Services.Base;
using jotfi.Jot.Database.Classes;
using jotfi.Jot.Database.Repository.System;
using jotfi.Jot.Model.System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace jotfi.Jot.Core.Services.System
{
    public partial class UserService : BaseService<UserService, UserRepository, User>
    {
        public UserService(IServiceProvider services) : base(services)
        {
        }

        public override Task<User> GetAsync(object id, UnitOfWork? uow = null)
        {
            if (Settings.IsClient)
            {
                return Task.Run(async () =>
                {
                    var response = await Client.GetAsync($"user/{id}");
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception("Cannot retrieve user");
                    }
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<User>(content);
                });
            }
            return base.GetAsync(id, uow);
        }

        public User? Authenticate(string username, string password)
        {
            if (Settings.IsClient)
            {
                return AuthenticateClient(username, password).Result;
            }
            var user = Repository.FirstOrDefault(p => p.UserName == username);
            if (!PasswordUtils.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }
            return user;
        }

        public bool GetPasswordValid(string? password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return false;
            }
            var passwordScore = PasswordUtils.CheckStrength(password);
            return passwordScore switch
            {
                PasswordScore.Blank => false,
                PasswordScore.VeryWeak => false,
                PasswordScore.Weak => false,
                _ => true
            };
        }

        public string GetPasswordInfo(string? password)
        {
            var passwordScore = PasswordUtils.CheckStrength(password);
            var passwordInfo = $"Password Strength: {passwordScore}.";
            if (!GetPasswordValid(password))
            {
                passwordInfo += "Please add extra length and/or complexity.";
            }
            return passwordInfo;
        }

        public bool GetEmailValid(string? email)
        {
            return ValidUtils.IsEmailValid(email);
        }

        public long CreateUser(User user)
        {
            try
            {
                if (Settings.IsClient)
                {
                    return CreateUserClient(user).Result;
                }
                return Repository.Insert(user);
            }
            catch (Exception ex)
            {
                Log.LogError(ex, ex.Message);
                return 0;
            }            
        }

    }
}
