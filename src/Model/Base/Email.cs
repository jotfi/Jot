
using jotfi.Jot.Base.System;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace jotfi.Jot.Model.Base
{
    public class Email : SubTransaction
    {
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; } = "";
        [NotMapped]
        [Display(Name = "Confirm Email")]
        public string ConfirmEmail { get; set; } = "";

        public override string CreateTable(DbDialects dialect = DbDialects.SQLite)
        {
            return $@"
create table {TableName()}(
{TransactionFields()},
{SubTransactionFields()},
EmailAddress text not null);"; 
        }
    }
}
