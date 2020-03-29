// Copyright 2020 John Cottrell
//
// This file is part of Jot.
//
// Jot is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Jot is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with Jot.  If not, see <https://www.gnu.org/licenses/>.

using jotfi.Jot.Core.Services.Base;
using jotfi.Jot.Database;
using jotfi.Jot.Database.Classes;
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

        protected DbContext GetContext()
        {
            return new DbContext(App.AppSettings.Database);
        }
    }
}
