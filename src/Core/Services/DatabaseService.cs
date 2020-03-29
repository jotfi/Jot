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

using Dapper;
using Dapper.FluentMap;
using Dapper.FluentMap.Dommel;
using jotfi.Jot.Base.System;
using jotfi.Jot.Database.Primitives;
using jotfi.Jot.Model.Base;
using System.Collections.Generic;

namespace jotfi.Jot.Core.Services
{
    
    public class DatabaseService : Logger
    {
        public readonly Application App;

        public DatabaseService(Application app, LogOpts opts = null) : base(opts)
        {
            App = app;
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new AddressMap());
                config.ForDommel();
            });
        }
    }
}
