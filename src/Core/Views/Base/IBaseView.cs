
using jotfi.Jot.Core.ViewModels.Base;

namespace jotfi.Jot.Core.Views.Base
{
    public interface IBaseView
    {
        Application GetApp();
        BaseViewModel GetViewModel();
    }
}
