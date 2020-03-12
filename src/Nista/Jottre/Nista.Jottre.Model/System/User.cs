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

        public static string CreateTable() =>
        $@"
create table User( 
    {TransactionFields()}
    UserName varchar(100) not null,
    PersonId integer,
    PasswordId integer,
);";
    }
}
