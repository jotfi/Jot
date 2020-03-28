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

using jotfi.Jot.Base.System;

namespace jotfi.Jot.Core.Services.System
{
    public class SystemServices : Logger
    {
        public readonly Application App;
        public readonly SetupService Setup;
        public readonly UserService User;
        public readonly OrganizationService Organization;
        public readonly LoginService Login;

        public SystemServices(Application app, LogOpts opts = null) : base(opts)
        {
            App = app;
            Setup = new SetupService(app, opts);
            User = new UserService(app, opts);
            Organization = new OrganizationService(app, opts);
            Login = new LoginService(app, opts);
        }
    }
}
