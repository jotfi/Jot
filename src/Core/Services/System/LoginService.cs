#region License
//
// Copyright (c) 2020, John Cottrell <me@john.co.com>
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
//
#endregion

using jotfi.Jot.Core.Services.Base;
using jotfi.Jot.Database.Repository.System;
using System;

namespace jotfi.Jot.Core.Services.System
{
    public partial class LoginService : BaseService<LoginService, UserRepository>, IService
    {
        public LoginService(IServiceProvider services) : base(services)
        {

        }

        public bool PerformLogin()
        {
            return false;
        }
    }
}
