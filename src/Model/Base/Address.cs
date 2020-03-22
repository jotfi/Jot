using jotfi.Jot.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace jotfi.Jot.Model.Base
{
    public class Address : EntityData
    {
        public string Lot { get; set; } = "";
        public string Unit { get; set; } = "";
        public string Number { get; set; } = "";
        public string Street { get; set; } = "";
        public string City { get; set; } = "";
        public string State { get; set; } = "";
        public string PostCode { get; set; } = "";
        public string Country { get; set; } = "";

        public override string CreateTable()
        {
            return $@"
create table {TableName()}(
{TransactionFields()},
{EntityDataFields()},
Lot varchar(50) not null, 
Unit varchar(50) not null, 
Number varchar(50) not null, 
Street text not null, 
City text not null, 
State text not null, 
PostCode varchar(50) not null, 
Country text not null);";
        }
    }
}
