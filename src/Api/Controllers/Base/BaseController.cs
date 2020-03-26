using jotfi.Jot.Core.Services.Base;
using jotfi.Jot.Database;
using Microsoft.AspNetCore.Mvc;

namespace jotfi.Jot.Api.Controllers.Base
{
    public class BaseController<T> : ControllerBase
    {
        protected Core.Application App;
        protected readonly T Service;

        public BaseController(Core.Application app, T service)
        {
            App = app;
            Service = service;
        }
    }
}
