using System;
using System.Collections.Generic;
using System.Text;

namespace jotfi.Jot.Model.Base
{
    public abstract class Entity : Transaction, ITransaction
    {
        public string Code { get; set; } = "";
        public string Description { get; set; } = "";

        public static string EntityFields()
        {
            return @"
Code varchar(100) not null,
Description text not null";
        }
    }
}
