using Nista.Jottre.Base.System;
using Nista.Jottre.Data.Base;
using Nista.Jottre.Model.System;

namespace Nista.Jottre.Data.System
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(RepositoryController data, LogOpts opts = null) : base(data, opts)
        {

        }

    }
}
