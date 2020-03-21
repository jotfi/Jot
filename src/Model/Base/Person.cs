using jotfi.Jot.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace jotfi.Jot.Model.Base
{
    public class Person : Entity 
    {
        public Person(string code = "", string description = "") : base(code, description)
        {

        }

        public string FirstNames { get; set; } = "";
        public string LastName { get; set; } = "";
        public long EmailId { get; set; }
        public Email Email { get; set; } = new Email();
        public long AddressId { get; set; }
        public Address Address { get; set; } = new Address();

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
