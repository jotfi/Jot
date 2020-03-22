﻿using jotfi.Jot.Base.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace jotfi.Jot.Data.Base
{
    public class BaseRepositories : Logger
    {
        public readonly RepositoryController Data;
        public readonly AddressRepository Address;
        public readonly EmailRepository Email;
        public readonly PasswordRepository Password;
        public readonly PersonRepository Person;

        public BaseRepositories(RepositoryController data, LogOpts opts = null) : base(opts)
        {
            Data = data;
            Address = new AddressRepository(data, opts);
            Email = new EmailRepository(data, opts);
            Password = new PasswordRepository(data, opts);
            Person = new PersonRepository(data, opts);
        }
    }
}
