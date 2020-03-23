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

        public Task<IEnumerable<T>> GetListAsync(object whereConditions = null)
        {
            try
            {
                using (Data.Db.Context.Create())
                {
                    return Data.Db.Context.GetConnection().GetListAsync<T>(whereConditions);
                }
            }
            catch (Exception ex)
            {
                Log(ex);
            }
            return null;
        }
    }
}
