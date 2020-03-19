using Nista.Jottre.Core.ViewModels.System;
using Nista.Jottre.Core.Views.Base;

namespace Nista.Jottre.Core.Views.System
{
    public interface ILoginViews : IBaseView
    {
        LoginViewModel GetLoginViewModel();
        bool PerformLogin();
        void AddMainMenu();

    }
}
