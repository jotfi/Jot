using System;
using System.Collections.Generic;
using System.Text;

namespace jotfi.Jot.Model.Base
{
    public class Password : EntityData
    {
        public string CreatePassword { get; set; } = "";
        public string ConfirmPassword { get; set; } = "";
        public string PasswordHash { get; set; }
        public string SecurityQuestion1 { get; set; }
        public string SecurityQuestion2 { get; set; }
        public string SecurityQuestion3 { get; set; }
        public string SecurityAnswer1 { get; set; }
        public string SecurityAnswer2 { get; set; }
        public string SecurityAnswer3 { get; set; }

        public override string CreateTable()
        {
            return $@"
create table {TableName()}(
{TransactionFields()},
{EntityDataFields()},
PasswordHash varchar(64) not null, 
SecurityQuestion1 text not null,
SecurityQuestion2 text not null,
SecurityQuestion3 text not null,
SecurityAnswer1 text not null,
SecurityAnswer2 text not null,
SecurityAnswer3 text not null);";
        }
    }
}
