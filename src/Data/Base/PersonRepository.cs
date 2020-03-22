using jotfi.Jot.Base.System;
using jotfi.Jot.Model.Base;

namespace jotfi.Jot.Data.Base
{
    public class PersonRepository : BaseRepository<Person>
    {
        public PersonRepository(RepositoryController data, LogOpts opts = null) : base(data, opts)
        {

        }
    }
}
