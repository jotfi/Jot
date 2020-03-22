using jotfi.Jot.Base.System;
using jotfi.Jot.Model.Base;

namespace jotfi.Jot.Data.Base
{
    public class EmailRepository : BaseRepository<Email>
    {
        public EmailRepository(RepositoryController data, LogOpts opts = null) : base(data, opts)
        {

        }
    }
}
