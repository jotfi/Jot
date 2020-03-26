using jotfi.Jot.Base.System;
using System;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace jotfi.Jot.Model.Base
{
    public abstract class Transaction : ITransaction
    {
        public long Id { get; set; }

        [JsonIgnore]
        public string Hash { get; set; } = "";

        [ReadOnly(true)]
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; } = DateTime.MinValue;

        public static string TransactionFields()
        {
            return @"
Id integer primary key autoincrement, 
Hash varchar(64) not null, 
CreatedDate datetime default current_timestamp, 
ModifiedDate datetime";
        }

        public virtual string TableName()
        {
            return GetType().Name;
        }

        public abstract string CreateTable(DbDialects dialect = DbDialects.SQLite);
    }
}
