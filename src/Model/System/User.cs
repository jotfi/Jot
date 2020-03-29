// Copyright 2020 John Cottrell
//
// This file is part of Jot.
//
// Jot is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Jot is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with Jot.  If not, see <https://www.gnu.org/licenses/>.

using jotfi.Jot.Base.System;
using jotfi.Jot.Model.Base;
using jotfi.Jot.Model.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace jotfi.Jot.Model.System
{
    public class User : Transaction
    {
        public string UserName { get; set; } = "";
        [JsonIgnore]
        public byte[] PasswordHash { get; set; }
        [JsonIgnore]
        public byte[] PasswordSalt { get; set; }
        public long PersonId { get; set; }
        public Person Person { get; set; } = new Person();

        [NotMapped]
        public string Token { get; set; }

        [NotMapped]
        [Display(Name = "User Password")]
        public string CreatePassword { get; set; } = "";

        [NotMapped]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; } = "";

        public override string CreateTable(DbConnectionTypes dialect = DbConnectionTypes.SQLite)
        {
            return $@"
create table {TableName()}(
{TransactionFields()},
UserName varchar(100) not null, 
PersonId integer, 
PasswordId integer);";
        }
    }
}
