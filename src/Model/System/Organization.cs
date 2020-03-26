using jotfi.Jot.Base.System;
using jotfi.Jot.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace jotfi.Jot.Model.System
{
    public class Organization : Entity
    {
        [Display(Name = "Organization Name")]
        public string Name { get; set; } = "";
        public bool CanLogin { get; set; }
        public List<Person> Contacts { get; set; }

        public override string CreateTable(DbDialects dialect = DbDialects.SQLite)
        {
            return $@"
create table {TableName()}(
{TransactionFields()}, 
{EntityFields()}, 
Name text not null,
CanLogin integer);";
        }
    }
}
