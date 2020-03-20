
using johncocom.Jot.Core.ViewModels.Base;

namespace johncocom.Jot.Core.Views.Base
{
    public interface IBaseView
    {
        Application GetApp();
        BaseViewModel GetViewModel();
    }
}
