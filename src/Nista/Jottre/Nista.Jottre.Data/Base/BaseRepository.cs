using Nista.Jottre.Database.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nista.Jottre.Data.Base
{
    public abstract class BaseRepository
    {
        protected readonly IConnectionContext Context;

        public BaseRepository(IConnectionContext context)
        {
            Context = context;
        }
    }
}
