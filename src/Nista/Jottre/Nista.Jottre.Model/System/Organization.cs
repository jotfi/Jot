using Nista.Jottre.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nista.Jottre.Model.System
{
    public class Organization : Entity, ITransaction
    {
        public string Name { get; set; }
        public List<Entity> Contacts { get; set; }

        public static string CreateTable()
        {
            return $@"create table if not exists Organization({TransactionFields()}, {EntityFields()}, Name text not null);";
        }
    }
}
