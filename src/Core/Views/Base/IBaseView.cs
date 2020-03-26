
using jotfi.Jot.Core.ViewModels;

namespace jotfi.Jot.Core.Views.Base
{
    public interface IBaseView<T>
    {
        Application App { get; }
        T ViewModel { get; }
        ViewModelFactory ViewModels { get => App.ViewModels; }
    }
}
