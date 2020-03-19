using Nista.Jottre.Core.ViewModels.System;
using Nista.Jottre.Core.Views.Base;

namespace Nista.Jottre.Core.Views.System
{
    public interface IStartViews : IBaseView
    {
        StartViewModel GetStartViewModel();
        void ApplicationStart();
        void SetupAdmin();
    }
}
