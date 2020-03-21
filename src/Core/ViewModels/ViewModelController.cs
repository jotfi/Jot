using jotfi.Jot.Base.System;
using jotfi.Jot.Core.ViewModels.System;
using jotfi.Jot.Core.Views.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace jotfi.Jot.Core.ViewModels
{
    public class ViewModelController : Logger
    {
        public readonly Application App;
        public readonly StartViewModel Start;
        public readonly UserViewModel User;
        public readonly LoginViewModel Login;

        public ViewModelController(Application app, LogOpts opts = null) : base(opts)
        {
            App = app;
            Start = new StartViewModel(app, opts);
            User = new UserViewModel(app, opts);
            Login = new LoginViewModel(app, opts);
        }

        //
        // Ensure ViewController has all required Views
        //
        public void SetupViews()
        {
            foreach (var view in App.Views.Items)
            {
                if (view == null)
                {
                    throw new NotImplementedException();
                }
            }
            Start.SetView(App.Views.Start);
            User.SetView(App.Views.User);
            Login.SetView(App.Views.Login);
        }
    }
}
