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
using System;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace jotfi.Jot.Model.Base
{
    public abstract class Transaction : ITransaction
    {
        public long Id { get; set; }

        [JsonIgnore]
        public string Hash { get; set; } = "";

        [ReadOnly(true)]
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; } = DateTime.MinValue;

        public static string TransactionFields()
        {
            return @"
Id integer primary key autoincrement, 
Hash varchar(64) not null, 
CreatedDate datetime default current_timestamp, 
ModifiedDate datetime";
        }

        public virtual string TableName()
        {
            return GetType().Name;
        }

        public abstract string CreateTable(DbConnectionTypes dialect = DbConnectionTypes.SQLite);
    }
}
