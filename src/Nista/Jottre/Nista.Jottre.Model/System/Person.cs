using Nista.Jottre.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nista.Jottre.Model.System
{
    public class Person : Entity, ITransaction
    {
        public string FirstName { get; set; }
        public string MiddleNames { get; set; }
        public string LastName { get; set; }
        public Email Email { get; set; }

        public static string CreateTable() =>
        $@"
create table Person( 
    {TransactionFields()}
    FirstName varchar(255) not null,
    MiddleNames varchar(255) not null,
    LastName varchar(255) not null,
    EmailId integer,
);";
    }
}
