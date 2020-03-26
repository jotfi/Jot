using jotfi.Jot.Base.System;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace jotfi.Jot.Model.Base
{
    public class Password : SubTransaction
    {
        [JsonIgnore]
        public byte[] PasswordHash { get; set; }

        [JsonIgnore]
        public byte[] PasswordSalt { get; set; }

        [NotMapped]        
        [Display(Name = "User Password")]
        public string CreatePassword { get; set; } = "";
        
        [NotMapped]        
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; } = "";


        public override string CreateTable(DbDialects dialect = DbDialects.SQLite)
        {
            return $@"
create table {TableName()}(
{TransactionFields()},
{SubTransactionFields()},
PasswordHash blob not null,
PasswordSalt blob not null);";
        }
    }
}
