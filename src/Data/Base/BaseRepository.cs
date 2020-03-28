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
using jotfi.Jot.Model.Base;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace jotfi.Jot.Data.Base
{
    public abstract partial class BaseRepository<T> : Logger where T : Transaction
    {
        protected readonly RepositoryFactory Data;

        public BaseRepository(RepositoryFactory data, LogOpts opts = null) : base(opts)
        {
            Data = data;
        }

        public virtual bool Exists() => GetCount() > 0;
        public virtual int GetCount() => GetList().Count();

        public IEnumerable<T> GetList(object whereConditions = null, DbConnection conn = null)
        {
            if (conn != null)
            {
                return conn.GetList<T>(whereConditions);
            }
            using (Data.Context.Create())
            {
                return Data.Context.GetConnection().GetList<T>(whereConditions);
            }
        }

        public virtual T Get(object id, DbConnection conn = null)
        {
            if (conn != null)
            {
                return conn.Get<T>(id);
            }
            using (Data.Context.Create())
            {
                return Data.Context.GetConnection().Get<T>(id);
            }
        }

        public virtual long Insert(T obj, DbConnection conn = null)
        {
            obj.Init();
            if (conn != null)
            {
                return conn.Insert<long, T>(obj);
            }
            using (Data.Context.Create())
            {
                return Data.Context.GetConnection().Insert<long, T>(obj);
            }
        }

        public virtual int Update(T obj, DbConnection conn = null)
        {
            obj.Init();
            if (conn != null)
            {
                return conn.Update(obj);
            }
            using (Data.Context.Create())
            {
                return Data.Context.GetConnection().Update(obj);
            }
        }
    }
}
