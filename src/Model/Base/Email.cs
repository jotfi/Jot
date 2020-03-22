
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace jotfi.Jot.Model.Base
{
    public class Email : SubTransaction
    {
        public string EmailAddress { get; set; } = "";

        [NotMapped]
        public string ConfirmEmail { get; set; } = "";

        public override string CreateTable()
        {
            return $@"
create table {TableName()}(
{TransactionFields()},
{SubTransactionFields()},
EmailAddress text not null);"; 
        }
    }
}
