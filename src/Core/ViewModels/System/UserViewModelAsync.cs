
using jotfi.Jot.Model.System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace jotfi.Jot.Core.ViewModels.System
{
    public partial class UserViewModel
    {
        public Task<IEnumerable<User>> GetUsersAsync()
        {
            return GetRepository().System.User.GetListAsync();
        }

    }
}
