using jotfi.Jot.Base.System;

namespace jotfi.Jot.Core.ViewModels.System
{
    public class SystemViewModels : Logger
    {
        public readonly Application App;
        public readonly SetupViewModel Setup;
        public readonly UserViewModel User;
        public readonly LoginViewModel Login;

        public SystemViewModels(Application app, LogOpts opts = null) : base(opts)
        {
            App = app;
            Setup = new SetupViewModel(app, opts);
        }
    }
}
