using Nista.Jottre.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nista.Jottre.Model.System
{
    public class Organization : Entity
    {
        public string Name { get; set; }
        public List<Entity> Contacts { get; set; }

        public override string CreateTable()
        {
            return $@"
create table {TableName()}(
{TransactionFields()}, 
{EntityFields()}, 
Name text not null);";
        }
    }
}
