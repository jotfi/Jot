using jotfi.Jot.Base.System;
using jotfi.Jot.Core.ViewModels.System;
using jotfi.Jot.Core.Views.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace jotfi.Jot.Core.ViewModels
{
    public class ViewModelFactory : Logger
    {
        public readonly Application App;
        public readonly SystemViewModels System;

        public ViewModelFactory(Application app, LogOpts opts = null) : base(opts)
        {
            App = app;
            System = new SystemViewModels(app, opts);
        }
    }
}
