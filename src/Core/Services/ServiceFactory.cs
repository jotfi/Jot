﻿// Copyright 2020 John Cottrell
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

using jotfi.Jot.Base.System;
using jotfi.Jot.Core.Services.System;

namespace jotfi.Jot.Core.Services
{
    public class ServiceFactory : Logger
    {
        public readonly Application App;
        public readonly SystemServices System;

        public ServiceFactory(Application app, LogOpts opts = null) : base(opts)
        {
            App = app;
            System = new SystemServices(app, opts);
        }
    }
}