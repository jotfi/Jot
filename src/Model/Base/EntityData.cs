using System;
using System.Collections.Generic;
using System.Text;

namespace jotfi.Jot.Model.Base
{
    public abstract class EntityData : Transaction
    {
        public long EntityId { get; set; }
        public string EntityType { get; set; } = "";

        public static string EntityDataFields()
        {
            return @"
EntityId integer,
EntityType varchar(100) not null";
        }
    }
}
