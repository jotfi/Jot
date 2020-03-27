using jotfi.Jot.Base.Classes;
using jotfi.Jot.Model.Base;
using jotfi.Jot.Model.System;
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
            var response = await App.Client.PostAsync("authenticate", auth.ToContent());
            response.EnsureSuccessStatusCode();
            var content = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<User>(content);
        }

        public async Task<User> GetUserByIdClient(long id)
        {
            var response = await App.Client.GetAsync($"user/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve tasks");
            }

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<User>(content);
        }

        public async Task<User> GetUserByNameClient(string name)
        {
            var response = await App.Client.GetAsync($"user/{name}");
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
                var response = await App.Client.PostAsync("user", user.ToContent());
                response.EnsureSuccessStatusCode();
                var newUserJson = response.Content.ReadAsStringAsync().Result;
                var newUser = JsonConvert.DeserializeObject<User>(newUserJson);
                return newUser.Id;
            }
            catch (Exception ex)
            {
                Log(ex);
                return 0;
            }            
        }
    }
}
