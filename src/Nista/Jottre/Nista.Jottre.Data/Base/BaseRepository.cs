using Nista.Jottre.Database.Base;
using Nista.Jottre.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nista.Jottre.Data.Base
{
    public abstract class BaseRepository<T> where T : Transaction
    {
        protected readonly IDbContext Context;

        public BaseRepository(IDbContext context)
        {
            Context = context;
        }

        public virtual bool Exists()
        {
            return (GetCount() == 0) ? false : true;
        }

        public virtual int GetCount()
        {
            int getCount = 0;
            using (Context.Create())
            {
                var list = Context.GetConnection().GetList<T>();
                getCount = list.Count();
            }
            return getCount;
        }
    }
}
