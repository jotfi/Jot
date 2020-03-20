using johncocom.Jot.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace johncocom.Jot.Model.Base
{
    public class Person : Entity 
    {
        public string FirstNames { get; set; }
        public string LastName { get; set; }
        public long EmailId { get; set; }
        public long AddressId { get; set; }

        public override string CreateTable()
        {
            return $@"
create table {TableName()}(
{TransactionFields()}, 
{EntityFields()}, 
FirstNames varchar(255) not null, 
LastName varchar(255) not null, 
EmailId integer,
AddressId integer);";
        }       
    }
}
