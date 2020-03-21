using jotfi.Jot.Core.ViewModels.System;
using jotfi.Jot.Core.Views.Base;

namespace jotfi.Jot.Core.Views.System
{
    public interface ILoginView : IBaseView
    {
        LoginViewModel GetLoginViewModel();
        bool PerformLogin();
        void AddMainMenu();

    }
}
