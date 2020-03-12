using Nista.Jottre.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nista.Jottre.Model.System
{
    public class Organization : Entity, ITransaction
    {
        public string Name { get; set; }
        public HashSet<Entity> Contacts { get; set; }

        public static string CreateTable() =>
            $@"
create table Organization( 
    {TransactionFields()}
    Name text not null
);";

    }
}
