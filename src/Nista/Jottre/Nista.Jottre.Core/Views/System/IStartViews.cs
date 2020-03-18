using Nista.Jottre.Core.Views.Base;

namespace Nista.Jottre.Core.Views.System
{
    public interface IStartViews : IBaseView
    {
        void ApplicationStart();
        void SetupAdmin();
    }
}
