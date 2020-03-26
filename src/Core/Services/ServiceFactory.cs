using jotfi.Jot.Base.System;
using jotfi.Jot.Core.Services.System;

namespace jotfi.Jot.Core.Services
{
    public class ServiceFactory : Logger
    {
        public readonly Application App;
        public readonly SystemServices System;

        public ServiceFactory(Application app, LogOpts opts = null) : base(opts)
        {
            App = app;
            System = new SystemServices(app, opts);
        }
    }
}
