using jotfi.Jot.Base.System;
using jotfi.Jot.Console.Views.Base;
using jotfi.Jot.Core.Services.Base;
using jotfi.Jot.Core.Services.System;
using jotfi.Jot.Core.Views.System;
using System;
using System.Collections.Generic;
using System.Text;
using Terminal.Gui;

namespace jotfi.Jot.Console.Views.System
{
    public class UserView : BaseView<UserService>, IUserView
    {

        public UserView(Core.Application app, UserService service, LogOpts opts = null)
            : base(app, service, opts)
        {
            
        }

        public void CreateNewUser()
        {
            throw new NotImplementedException();
        }
    }
}
