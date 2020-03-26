using jotfi.Jot.Core.Services.Base;
using jotfi.Jot.Database;
using Microsoft.AspNetCore.Mvc;

namespace jotfi.Jot.Api.Controllers.Base
{
    public class BaseController<T> : ControllerBase
    {
        protected readonly T Service;

        public BaseController(T service)
        {
            Service = service;
        }
    }
}
