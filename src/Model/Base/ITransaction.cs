using jotfi.Jot.Base.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace jotfi.Jot.Model.Base
{
    public interface ITransaction
    {
        string TableName();
        string CreateTable(DbDialects dialect = DbDialects.SQLite);
    }
}
