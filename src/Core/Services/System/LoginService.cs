using jotfi.Jot.Base.System;
using jotfi.Jot.Core.Services.Base;

namespace jotfi.Jot.Core.Services.System
{
    public partial class LoginService : BaseService
    {
        public LoginService(Application app, LogOpts opts = null) : base(app, opts)
        {

        }

        public bool PerformLogin()
        {
            return false;
        }

        

    }
}
