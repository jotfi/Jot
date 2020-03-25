using jotfi.Jot.Core.ViewModels.Base;
using jotfi.Jot.Database;
using Microsoft.AspNetCore.Mvc;

namespace jotfi.Jot.Api.Controllers.Base
{
    public class BaseController<T> : ControllerBase
    {
        protected readonly T ViewModel;

        public BaseController(T viewmodel)
        {
            ViewModel = viewmodel;
        }
    }
}
