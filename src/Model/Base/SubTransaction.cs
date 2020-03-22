using System;
using System.Collections.Generic;
using System.Text;

namespace jotfi.Jot.Model.Base
{
    public abstract class SubTransaction : Transaction
    {
        public long TxId { get; set; }
        public string TxType { get; set; } = "";

        public static string SubTransactionFields()
        {
            return @"
TxId integer,
TxType varchar(100) not null";
        }
    }
}
