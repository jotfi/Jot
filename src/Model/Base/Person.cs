using jotfi.Jot.Base.System;
using jotfi.Jot.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace jotfi.Jot.Model.Base
{
    public class Person : Transaction 
    {
        public string FirstNames { get; set; } = "";
        public string LastName { get; set; } = "";
        public long EmailId { get; set; }
        public Email Email { get; set; } = new Email();
        public long AddressId { get; set; }
        public Address Address { get; set; } = new Address();

        public override string CreateTable(DbDialects dialect = DbDialects.SQLite)
        {
            return $@"
create table {TableName()}(
{TransactionFields()},
FirstNames varchar(255) not null, 
LastName varchar(255) not null, 
EmailId integer,
AddressId integer);";
        }       
    }
}
