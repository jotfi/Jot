using jotfi.Jot.Base.System;
using jotfi.Jot.Model.Base;

namespace jotfi.Jot.Data.Base
{
    public class PersonRepository : BaseRepository<Person>
    {
        public PersonRepository(RepositoryFactory data, LogOpts opts = null) : base(data, opts)
        {

        }
    }
}
