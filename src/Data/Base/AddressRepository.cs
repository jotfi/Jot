using jotfi.Jot.Base.System;
using jotfi.Jot.Model.Base;

namespace jotfi.Jot.Data.Base
{
    public class AddressRepository : BaseRepository<Address>
    {
        public AddressRepository(RepositoryController data, LogOpts opts = null) : base(data, opts)
        {

        }

    }
}
