
using jotfi.Jot.Base.Utils;
using jotfi.Jot.Model.Base;
using jotfi.Jot.Model.System;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace jotfi.Jot.Core.ViewModels.System
{
    public partial class UserViewModel
    {
        public User AuthenticateClient(string username, string password)
        {
            var auth = new Authenticate() { Username = username, Password = password };
            var response = App.Client.PostAsync("authenticate", auth.ToContent()).Result;
            response.EnsureSuccessStatusCode();
            var user = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<User>(user);
        }

        public bool CreateUserClient(User user)
        {
            try
            {
                var response = App.Client.PostAsync("user", user.ToContent()).Result;
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception ex)
            {
                Log(ex);
                return false;
            }            
        }
    }
}
