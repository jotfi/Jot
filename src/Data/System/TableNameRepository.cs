using jotfi.Jot.Base.System;
using jotfi.Jot.Data.Base;
using jotfi.Jot.Model.System;

namespace jotfi.Jot.Data.System
{
    public class TableNameRepository : BaseRepository<TableName>
    {
        public TableNameRepository(RepositoryFactory data, LogOpts opts = null) : base(data, opts)
        {

        }

    }
}
