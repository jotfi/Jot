﻿using jotfi.Jot.Base.System;
using jotfi.Jot.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace jotfi.Jot.Model.System
{
    public class User : Transaction
    {
        public string UserName { get; set; } = "";
        public long PersonId { get; set; }
        public Person Person { get; } = new Person();
        public long PasswordId { get; set; }
        public Password Password { get; } = new Password();

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
