
using jotfi.Jot.Core.ViewModels;

namespace jotfi.Jot.Core.Views.Base
{
    public interface IBaseView<T>
    {
        Application GetApp();
        T GetViewModel();
        ViewModelFactory GetViewModels();
        void Quit();
    }
}
