
using jotfi.Jot.Base.Utils;
using jotfi.Jot.Model.System;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace jotfi.Jot.Core.ViewModels.System
{
    public partial class UserViewModel
    {
        public bool CreateUserClient(User user)
        {
            try
            {
                var client = GetApp().Client;
                var response = client.PostAsync("user", user.ToContent()).Result;
                response.EnsureSuccessStatusCode();
                // return URI of the created resource.
                //return response.Headers.Location;
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
