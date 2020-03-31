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
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace jotfi.Jot.Database.Classes
{
    public static class Extensions
    {

        public static void Init(this Transaction transaction)
        {
            transaction.ModifiedAt = DateTime.Now;
            transaction.Hash = HashUtils.GetSHA256Hash(transaction.ToJson());
        }

        public static long Insert<T>(this T transaction, IUnitOfWork unitOfWork) where T : Transaction
        {
            var repository = new Repository<T>(unitOfWork);
            var id = (long)repository.Insert(transaction);
            id.IsNotZero();
            return id;
        }

        public static Task<object> InsertAsync<T>(this T transaction, IUnitOfWork unitOfWork) where T : Transaction
        {
            var repository = new Repository<T>(unitOfWork);
            return repository.InsertAsync(transaction);
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
                .WithColumn("CreatedAt").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime)
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
                .WithColumn("Code").AsAnsiString(8).NotNullable()
                .WithColumn("Description").AsString(255).NotNullable();
        }
    }
}
