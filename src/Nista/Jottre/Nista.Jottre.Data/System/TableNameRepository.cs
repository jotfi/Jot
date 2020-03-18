using Nista.Jottre.Data.Base;
using Nista.Jottre.Database.Base;
using Nista.Jottre.Model.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nista.Jottre.Data.System
{
    public class TableNameRepository : BaseRepository<TableName>
    {
        public TableNameRepository(RepositoryController data,
            bool isConsole = true, Action<string> showLog = null) : base(data, isConsole, showLog)
        {

        }

    }
}
