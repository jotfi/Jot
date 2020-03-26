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

        public virtual Task<T> GetByIdAsync(long id, DbConnection conn = null)
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
