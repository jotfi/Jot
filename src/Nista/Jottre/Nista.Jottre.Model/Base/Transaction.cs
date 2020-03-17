using System;
using System.Collections.Generic;
using System.Text;

namespace Nista.Jottre.Model.Base
{
    public abstract class Transaction
    {
        public long Id { get; set; }
        public string Hash { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public static string TransactionFields()
        {
            return @"Id integer primary key autoincrement, Hash varchar(64) not null, CreatedDate datetime default current_timestamp, ModifiedDate datetime";
        }            
    }
}
