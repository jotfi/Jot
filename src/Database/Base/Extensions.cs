using jotfi.Jot.Base.Classes;
using jotfi.Jot.Base.Utils;
using jotfi.Jot.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace jotfi.Jot.Database.Base
{
    public static class Extensions
    {
        public static void Init(this Transaction transaction)
        {
            transaction.ModifiedDate = DateTime.Now;
            transaction.Hash = HashUtils.GetSHA256Hash(transaction.ToJson());
        }
    }
}
