
using jotfi.Jot.Core.Services;

namespace jotfi.Jot.Core.Views.Base
{
    public interface IBaseView<T>
    {
        Application App { get; }
        T Service { get; }
        ServiceFactory Services { get => App.Services; }
    }
}
