using johncocom.Jot.Core.ViewModels.System;
using johncocom.Jot.Core.Views.Base;

namespace johncocom.Jot.Core.Views.System
{
    public interface ILoginViews : IBaseView
    {
        LoginViewModel GetLoginViewModel();
        bool PerformLogin();
        void AddMainMenu();

    }
}
