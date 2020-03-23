using jotfi.Jot.Core.ViewModels.Base;
using jotfi.Jot.Database;
using Microsoft.AspNetCore.Mvc;

namespace jotfi.Jot.Api.Controllers.Base
{
    public class BaseController : ControllerBase
    {
        protected readonly BaseViewModel ViewModel;

        public BaseController(BaseViewModel viewmodel)
        {
            ViewModel = viewmodel;
        }
    }
}
