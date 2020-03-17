using Nista.Jottre.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nista.Jottre.Model.System
{
    public class User : Entity, ITransaction
    {
        public string UserName { get; set; }
        public Person Person { get; set; }
        public Password Password { get; set; }

        public override string CreateTable()
        {
            return $@"create table {TableName()}({TransactionFields()}, {EntityFields()}, UserName varchar(100) not null, PersonId integer, PasswordId integer);";
        }
    }
}
