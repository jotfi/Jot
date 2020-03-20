using johncocom.Jot.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace johncocom.Jot.Model.System
{
    public class User : Entity, ITransaction
    {
        public string UserName { get; set; }

        //Person contains 
        public Person Person { get; } = new Person();
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
