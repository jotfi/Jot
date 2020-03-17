using Nista.Jottre.Base;
using Nista.Jottre.Database.Base;
using Nista.Jottre.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nista.Jottre.Data.Base
{
    public abstract class BaseRepository<T> : Logger where T : Transaction
    {
        protected readonly IDbContext Context;

        public BaseRepository(IDbContext context)
        {
            Context = context;
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
                using (Context.Create())
                {
                    return Context.GetConnection().GetList<T>(whereConditions);
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
                using (Context.Create())
                {
                    return Context.GetConnection().Get<T>(id);
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
