
using Nista.Jottre.Core.ViewModels.Base;

namespace Nista.Jottre.Core.Views.Base
{
    public interface IBaseView
    {
        Application GetApp();
        BaseViewModel GetViewModel();
    }
}
