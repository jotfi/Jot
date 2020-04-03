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
using jotfi.Jot.Database.Classes;

namespace jotfi.Jot.Database.Migrations.Base
{
    [Migration(20200401)]
    public class AddInitialTables : Migration
    {
        public override void Up()
        {
            Create.Table("Persons")
                .WithEntityColumns()
                .WithColumn("FirstName").AsString(255).NotNullable()
                .WithColumn("LastName").AsString(255).NotNullable()
                .WithColumn("Contact").AsString().Nullable()
                .WithColumn("Address").AsString().Nullable();

            Create.Index("ix_Code").OnTable("Persons").OnColumn("Code")
                .Ascending().WithOptions().NonClustered();

            Create.Table("Users")
                .WithTransactionColumns()
                .WithColumn("UserName").AsString(50).NotNullable()
                .WithColumn("PasswordHash").AsBinary()
                .WithColumn("PasswordSalt").AsBinary()
                .WithColumn("PersonId").AsInt64().NotNullable();

            Create.ForeignKey("fk_Users_PersonId_Person_Id")
                .FromTable("Users").ForeignColumn("PersonId")
                .ToTable("Person").PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.Table("Address");
            Delete.Table("ContactDetails");
            Delete.Table("Persons");
            Delete.Table("Users");
        }
    }
}
