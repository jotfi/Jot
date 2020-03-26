using System;
using System.Collections.Generic;
using System.Text;

namespace jotfi.Jot.Core.Views.System
{
    public interface ISystemViews
    {
        Application App { get; }
        ISetupView Setup { get; }
        IUserView User { get; }
        ILoginView Login { get; }
    }
}
