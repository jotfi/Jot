#region License
//
// Copyright (c) 2020, John Cottrell <me@john.co.com>
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
//
#endregion
using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using jotfi.Jot.Base.Classes;
using jotfi.Jot.Base.System;
using jotfi.Jot.Base.Utils;
using jotfi.Jot.Model.Base;
using jotfi.Jot.Model.System;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace jotfi.Jot.Database.Classes
{
    public static class Extensions
    {
        public static void AddPasswordHash(this User user)
        {
            PasswordUtils.CreatePasswordHash(user.CreatePassword, out byte[] hash, out byte[] salt);
            user.PasswordHash = hash;
            user.PasswordSalt = salt;
        }
        public static T Hash<T>(this T transaction) where T : Transaction
        {
            transaction.ModifiedAt = DateTime.Now;
            transaction.Hash = HashUtils.GetSHA256Hash(transaction.ToJson());
            return transaction;
        }

        public static T HashEntity<T>(this T entity, long count) where T : Entity
        {
            entity.SetCode(count + 1);
            entity.Description ??= string.Empty;
            return entity.Hash();
        }

        public static ICreateTableColumnOptionOrWithColumnSyntax WithIdColumn(this ICreateTableWithColumnSyntax tableWithColumnSyntax)
        {
            return tableWithColumnSyntax
                .WithColumn("Id")
                .AsInt64()
                .NotNullable()
                .PrimaryKey()
                .Identity();
        }

        public static ICreateTableColumnOptionOrWithColumnSyntax WithTimeStamps(this ICreateTableWithColumnSyntax tableWithColumnSyntax)
        {
            return tableWithColumnSyntax
                .WithColumn("CreatedAt").AsDateTime().Nullable().WithDefault(SystemMethods.CurrentUTCDateTime)
                .WithColumn("ModifiedAt").AsDateTime().NotNullable();
        }

        public static ICreateTableColumnOptionOrWithColumnSyntax WithTransactionColumns(this ICreateTableWithColumnSyntax tableWithColumnSyntax)
        {
            return tableWithColumnSyntax
                .WithIdColumn()
                .WithColumn("Hash").AsAnsiString(64).NotNullable()
                .WithTimeStamps();
        }

        public static ICreateTableColumnOptionOrWithColumnSyntax WithEntityColumns(this ICreateTableWithColumnSyntax tableWithColumnSyntax)
        {
            return tableWithColumnSyntax
                .WithTransactionColumns()
                .WithColumn("Code").AsAnsiString(30).NotNullable()
                .WithColumn("CodePrefix").AsAnsiString(20).NotNullable()
                .WithColumn("Description").AsString(255).Nullable();
        }
    }
}
