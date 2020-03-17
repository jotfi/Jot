using Nista.Jottre.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nista.Jottre.Model.System
{
    public class Person : Entity, ITransaction
    {
        public string FirstNames { get; set; }
        public string LastName { get; set; }
        public Email Email { get; set; }

        public static string CreateTable()
        {
            return $@"create table if not exists Person({TransactionFields()}, {EntityFields()}, FirstNames varchar(255) not null, LastName varchar(255) not null, EmailId integer);";
        }       
    }
}
