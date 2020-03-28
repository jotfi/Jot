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
using System.Threading.Tasks;

namespace jotfi.Jot.Data.Base
{
    public abstract partial class BaseRepository<T> : Logger where T : Transaction
    {
        public virtual Task<bool> ExistsAsync()
        {
            return Task.Run(() =>
            {
                return GetCountAsync().Result > 0;
            });
        }

        public virtual Task<int> GetCountAsync()
        {
            return Task.Run(() =>
            {
                return GetListAsync().Result.Count();
            });
        }

        public virtual Task<IEnumerable<T>> GetListAsync(object whereConditions = null)
        {
            using (Data.Context.Create())
            {
                return Data.Context.GetConnection().GetListAsync<T>(whereConditions);
            }
        }

        public virtual Task<T> GetAsync(object id, DbConnection conn = null)
        {
            if (conn != null)
            {
                return conn.GetAsync<T>(id);
            }
            using (Data.Context.Create())
            {
                return Data.Context.GetConnection().GetAsync<T>(id);
            }
        }

        public virtual Task<long> InsertAsync(T obj, DbConnection conn = null)
        {
            obj.Init();
            if (conn != null)
            {
                return conn.InsertAsync<long, T>(obj);
            }
            using (Data.Context.Create())
            {
                return Data.Context.GetConnection().InsertAsync<long, T>(obj);
            }
        }

        public virtual Task<int> UpdateAsync(T obj, DbConnection conn = null)
        {
            obj.Init();
            if (conn != null)
            {
                return conn.UpdateAsync(obj);
            }
            using (Data.Context.Create())
            {
                return Data.Context.GetConnection().UpdateAsync(obj);
            }
        }
    }
}
