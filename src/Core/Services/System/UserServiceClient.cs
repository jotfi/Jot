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
using jotfi.Jot.Base.Classes;
using jotfi.Jot.Model.Base;
using jotfi.Jot.Model.Primitives;
using jotfi.Jot.Model.System;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace jotfi.Jot.Core.Services.System
{
    public partial class UserService
    {
        public async Task<User> AuthenticateClient(string username, string password)
        {
            var auth = new Authenticate() { Username = username, Password = password };
            var response = await Client.PostAsync("authenticate", auth.ToContent());
            response.EnsureSuccessStatusCode();
            var content = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<User>(content);
        }

        public async Task<User> GetUserByIdClient(long id)
        {
            var response = await Client.GetAsync($"user/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve tasks");
            }

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<User>(content);
        }

        public async Task<User> GetUserByNameClient(string name)
        {
            var response = await Client.GetAsync($"user/{name}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve tasks");
            }

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<User>(content);
        }

        public async Task<long> CreateUserClient(User user)
        {
            try
            {
                var response = await Client.PostAsync("user", user.ToContent());
                response.EnsureSuccessStatusCode();
                var newUserJson = response.Content.ReadAsStringAsync().Result;
                var newUser = JsonConvert.DeserializeObject<User>(newUserJson);
                return newUser.Id;
            }
            catch (Exception ex)
            {
                Log.LogError(ex, ex.Message);
                return 0;
            }            
        }
    }
}
