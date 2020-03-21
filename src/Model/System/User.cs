﻿using jotfi.Jot.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace jotfi.Jot.Model.System
{
    public class User : Entity, ITransaction
    {
        public User(string code = "", string description = "") : base(code, description)
        {
            UserName = code;
            Person.Code = code;
            Person.Description = description;
        }

        public string UserName { get; set; }
        public long PersonId { get; set; }
        public Person Person { get; } = new Person();
        public long PasswordId { get; set; }
        public Password Password { get; } = new Password();

        public override string CreateTable()
        {
            return $@"
create table {TableName()}(
{TransactionFields()}, 
{EntityFields()}, 
UserName varchar(100) not null, 
PersonId integer, 
PasswordId integer);";
        }
    }
}
