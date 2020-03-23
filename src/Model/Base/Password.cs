using jotfi.Jot.Base.System;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace jotfi.Jot.Model.Base
{
    public class Password : SubTransaction
    {
        public string PasswordHash { get; set; } = "";

        [NotMapped]
        public string CreatePassword { get; set; } = "";
        [NotMapped]
        public string ConfirmPassword { get; set; } = "";


        public override string CreateTable(DbDialects dialect = DbDialects.SQLite)
        {
            return $@"
create table {TableName()}(
{TransactionFields()},
{SubTransactionFields()},
PasswordHash varchar(64) not null);";
        }
    }
}
