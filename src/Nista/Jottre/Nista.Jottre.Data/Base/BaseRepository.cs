using Nista.Jottre.Base.Log;
using Nista.Jottre.Database.Base;
using Nista.Jottre.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nista.Jottre.Data.Base
{
    public abstract class BaseRepository<T> : Logging where T : Transaction
    {
        protected readonly RepositoryController Data;

        public BaseRepository(RepositoryController data, 
            bool isConsole = true, Action<string> showLog = null) : base(isConsole, showLog)
        {
            Data = data;
        }

        //Todo: support async methods

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

        public virtual T GetById(int id)
        {
            try
            {
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
    }
}
