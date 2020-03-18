using Nista.Jottre.Base.System;
using Nista.Jottre.Data.Base;
using Nista.Jottre.Model.System;

namespace Nista.Jottre.Data.System
{
    public class TableNameRepository : BaseRepository<TableName>
    {
        public TableNameRepository(RepositoryController data, LogOpts opts = null) : base(data, opts)
        {

        }

    }
}
