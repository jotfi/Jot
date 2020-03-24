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

        public virtual bool Exists()
        {
            return GetCount() > 0;
        }

        public virtual int GetCount()
        {
            return GetList().Count();
        }

        public IEnumerable<T> GetList(object whereConditions = null)
        {
            try
            {
                using (Data.Db.Context.Create())
                {
                    return Data.Db.Context.GetConnection().GetList<T>(whereConditions);
                }
            }
            catch (Exception ex)
            {
                Log(ex);
            }
            return new List<T>();
        }

        public virtual T GetById(long id, DbConnection conn = null)
        {
            try
            {
                if (conn != null)
                {
                    return conn.Get<T>(id);
                }
                using (Data.Db.Context.Create())
                {
                    return Data.Db.Context.GetConnection().Get<T>(id);
                }
            }
            catch (Exception ex)
            {
                Log(ex);
            }
            return null;
        }

        public virtual long Insert(T obj, DbConnection conn = null)
        {
            try
            {
                obj.Init();
                if (conn != null)
                {
                    return conn.Insert<long, T>(obj);
                }
                using (Data.Db.Context.Create())
                {
                    return Data.Db.Context.GetConnection().Insert<long, T>(obj);
                }
            }
            catch (Exception ex)
            {
                Log(ex);
            }
            return 0;
        }

        public virtual int Update(T obj, DbConnection conn = null)
        {
            try
            {
                obj.Init();
                if (conn != null)
                {
                    return conn.Update(obj);
                }
                using (Data.Db.Context.Create())
                {
                    return Data.Db.Context.GetConnection().Update(obj);
                }
            }
            catch (Exception ex)
            {
                Log(ex);
            }
            return 0;
        }
    }
}
