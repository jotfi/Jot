using jotfi.Jot.Base.System;
using jotfi.Jot.Core.Views.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace jotfi.Jot.Console.Views.System
{
    public class SystemViews : Logger, ISystemViews
    {
        public Core.Application App { get; }
        public ISetupView Setup { get; }
        public IUserView User { get; }
        public ILoginView Login { get; }

        public SystemViews(Core.Application app, LogOpts opts = null) : base(opts)
        {
            App = app;
            Setup = new SetupView(app, app.ViewModels.System.Setup, opts);
            User = new UserView(app, app.ViewModels.System.User, opts);
            Login = new LoginView(app, app.ViewModels.System.Login, opts);
        }
    }
}
