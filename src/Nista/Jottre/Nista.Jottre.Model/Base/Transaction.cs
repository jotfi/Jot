using System;
using System.Collections.Generic;
using System.Text;

namespace Nista.Jottre.Model.Base
{
    public abstract class Transaction
    {
        public long Id { get; set; }
        public string Hash { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime ModifiedTime { get; set; }

        public static string TransactionFields() =>
            @"
    Id integer primary key,
    Hash varchar(64) not null,
    CreatedTime text not null,
    ModifiedTime text not null";

    }
}
