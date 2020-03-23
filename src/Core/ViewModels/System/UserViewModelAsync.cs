
using jotfi.Jot.Model.System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace jotfi.Jot.Core.ViewModels.System
{
    public partial class UserViewModel
    {
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            var users = await GetRepository().System.User.GetListAsync();
            foreach (var user in users)
            {
                GetUserDetails(user);
            }
            return users;
        }

    }
}
