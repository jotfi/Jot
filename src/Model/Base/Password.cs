using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace jotfi.Jot.Model.Base
{
    public class Password : EntityData
    {
        public string PasswordHash { get; set; } = "";

        [NotMapped]
        public string CreatePassword { get; set; } = "";
        [NotMapped]
        public string ConfirmPassword { get; set; } = "";

        //public string SecurityQuestion1 { get; set; } = "";
        //public string SecurityQuestion2 { get; set; } = "";
        //public string SecurityQuestion3 { get; set; } = "";
        //public string SecurityAnswer1 { get; set; } = "";
        //public string SecurityAnswer2 { get; set; } = "";
        //public string SecurityAnswer3 { get; set; } = "";

        public override string CreateTable()
        {
            return $@"
create table {TableName()}(
{TransactionFields()},
{EntityDataFields()},
PasswordHash varchar(64) not null);";

//SecurityQuestion1 text,
//SecurityQuestion2 text,
//SecurityQuestion3 text,
//SecurityAnswer1 text,
//SecurityAnswer2 text,
//SecurityAnswer3 text

        }
    }
}
