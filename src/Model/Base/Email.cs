
using System;
using System.Collections.Generic;
using System.Text;

namespace johncocom.Jot.Model.Base
{
    public class Email : EntityData
    {
        public string EmailAddress { get; set; }

        public override string CreateTable()
        {
            return $@"
create table {TableName()}(
{TransactionFields()},
{EntityDataFields()},
EmailAddress text not null);"; 
        }
    }
}
