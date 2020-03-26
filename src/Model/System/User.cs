using jotfi.Jot.Base.System;
using jotfi.Jot.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace jotfi.Jot.Model.System
{
    public class User : Transaction
    {
        public string UserName { get; set; } = "";
        public long PersonId { get; set; }
        public Person Person { get; set; } = new Person();
        public long PasswordId { get; set; }
        public Password Password { get; set; } = new Password();

        [NotMapped]
        public string Token { get; set; }

        public override string CreateTable(DbDialects dialect = DbDialects.SQLite)
        {
            return $@"
create table {TableName()}(
{TransactionFields()},
UserName varchar(100) not null, 
PersonId integer, 
PasswordId integer);";
        }
    }
}
