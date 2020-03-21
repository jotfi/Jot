using jotfi.Jot.Base.System;
using jotfi.Jot.Console.Views.Base;
using jotfi.Jot.Core.ViewModels.Base;
using jotfi.Jot.Core.ViewModels.System;
using jotfi.Jot.Core.Views.System;
using System;
using System.Collections.Generic;
using System.Text;
using Terminal.Gui;

namespace jotfi.Jot.Console.Views.System
{
    public class UserView : BaseView, IUserView
    {

        public UserView(ConsoleApplication app, BaseViewModel vm, LogOpts opts = null)
            : base(app, vm, opts)
        {
            
        }

        public UserViewModel GetUserViewModel()
        {
            return (UserViewModel)GetViewModel();
        }

    }
}
