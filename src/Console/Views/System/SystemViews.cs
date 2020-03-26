﻿using jotfi.Jot.Base.System;
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
            Setup = new SetupView(app, app.Services.System.Setup, opts);
            User = new UserView(app, app.Services.System.User, opts);
            Login = new LoginView(app, app.Services.System.Login, opts);
        }
    }
}
