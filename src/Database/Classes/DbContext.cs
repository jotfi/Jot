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
using jotfi.Jot.Base.Settings;
using jotfi.Jot.Base.Utils;
using System;
using System.Data.Common;

namespace jotfi.Jot.Database.Classes
{
    public class DbContext : IDisposable
    {
        private readonly DbConnection Connection = null;
        public readonly UnitOfWork UnitOfWork = null;

        public DbContext(DbSettings settings)
        {
            Connection = DbUtils.CreateConnection(settings);
            Connection.Open();
            UnitOfWork = new UnitOfWork(Connection);
        }

        public void Dispose()
        {
            UnitOfWork.Dispose();
            Connection.Dispose();
        }
    }
}
