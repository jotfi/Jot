﻿using jotfi.Jot.Base.System;
using jotfi.Jot.Model.Base;

namespace jotfi.Jot.Data.Base
{
    public class PasswordRepository : BaseRepository<Password>
    {
        public PasswordRepository(RepositoryController data, LogOpts opts = null) : base(data, opts)
        {

        }
    }
}