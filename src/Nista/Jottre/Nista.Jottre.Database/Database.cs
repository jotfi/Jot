using Nista.Jottre.Base;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Nista.Jottre.Database
{
    public abstract class Database : Logger
    {
        protected DbConnection DbConnection;

        public virtual void Start()
        {
            Open();
            if (DbConnection.State == System.Data.ConnectionState.Open)
            {
                Setup();
            }
        }

        public virtual void Open()
        {

        }

        public virtual void Setup()
        {

        }
    }
}
