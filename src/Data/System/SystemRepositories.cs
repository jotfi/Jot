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
using jotfi.Jot.Database.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace jotfi.Jot.Data.System
{
    public class SystemRepositories : Logger
    {
        public readonly RepositoryFactory Data;
        public readonly OrganizationRepository Organization;
        public readonly TableNameRepository TableName;
        public readonly UserRepository User;

        public SystemRepositories(RepositoryFactory data, LogOpts opts = null) : base(opts)
        {
            Data = data;
            Organization = new OrganizationRepository(data, opts);
            TableName = new TableNameRepository(data, opts);
            User = new UserRepository(data, opts);
        }
    }
}
