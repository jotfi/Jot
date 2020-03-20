using johncocom.Jot.Base.System;
using johncocom.Jot.Data.Base;
using johncocom.Jot.Model.System;

namespace johncocom.Jot.Data.System
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(RepositoryController data, LogOpts opts = null) : base(data, opts)
        {

        }

    }
}
