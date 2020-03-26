using jotfi.Jot.Base.System;
using jotfi.Jot.Core.Views.Base;
using jotfi.Jot.Core.Views.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace jotfi.Jot.Core.Views
{
    public interface IViewFactory
    {
        Application App { get; }
        ISystemViews System { get; }
    }
}
