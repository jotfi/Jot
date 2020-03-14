using Nista.Jottre.Database.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nista.Jottre.Data.Base
{
    public abstract class Repository
    {
        protected readonly IConnectionContext Context;

        public Repository(IConnectionContext context)
        {
            Context = context;
        }
    }
}
