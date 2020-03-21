using jotfi.Jot.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace jotfi.Jot.Model.System
{
    public class ApplicationSettings : Transaction
    {
        public string InstanceName { get; set; }

        public override string CreateTable()
        {
            return $@"
create table {TableName()}(
{TransactionFields()},
InstanceName text not null);";
        }
    }
}
