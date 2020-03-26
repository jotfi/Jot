using jotfi.Jot.Base.System;

namespace jotfi.Jot.Core.Services.System
{
    public class SystemServices : Logger
    {
        public readonly Application App;
        public readonly SetupService Setup;
        public readonly UserService User;
        public readonly OrganizationService Organization;
        public readonly LoginService Login;

        public SystemServices(Application app, LogOpts opts = null) : base(opts)
        {
            App = app;
            Setup = new SetupService(app, opts);
            User = new UserService(app, opts);
            Organization = new OrganizationService(app, opts);
            Login = new LoginService(app, opts);
        }
    }
}
