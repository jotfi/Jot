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

namespace jotfi.Jot.Database.Migrations.Primitives
{
    [Migration(20200331)]
    public class AddInitialTables : Migration
    {
        public override void Up()
        {
            Create.Table("Address")
                .WithTransactionColumns()
                .WithColumn("Lot").AsString(50).NotNullable()
                .WithColumn("Unit").AsString(50).NotNullable()
                .WithColumn("Number").AsString(50).NotNullable()
                .WithColumn("Street").AsString(255).NotNullable()
                .WithColumn("City").AsString(255).NotNullable()
                .WithColumn("State").AsString(255).NotNullable()
                .WithColumn("PostCode").AsString(50).NotNullable()
                .WithColumn("Country").AsString(255).NotNullable();

            Create.Table("ContactDetails")
                .WithTransactionColumns()
                .WithColumn("EmailAddress").AsString(255).NotNullable()
                .WithColumn("MobilePhone").AsString(50).NotNullable()
                .WithColumn("HomePhone").AsString(50).NotNullable()
                .WithColumn("WorkPhone").AsString(50).NotNullable();

            Create.Table("Persons")
                .WithEntityColumns()
                .WithColumn("FirstName").AsString(255).NotNullable()
                .WithColumn("LastName").AsString(255).NotNullable()
                .WithColumn("ContactDetailId").AsInt64().NotNullable()
                .WithColumn("AddressId").AsInt64().NotNullable();

            Create.Index("ix_Code").OnTable("Persons").OnColumn("Code")
                .Ascending().WithOptions().NonClustered();

            Create.ForeignKey("fk_Persons_ContactDetailId_ContactDetail_Id")
                .FromTable("Persons").ForeignColumn("ContactDetailId")
                .ToTable("ContactDetail").PrimaryColumn("Id");

            Create.ForeignKey("fk_Persons_AddressId_Address_Id")
                .FromTable("Persons").ForeignColumn("AddressId")
                .ToTable("Address").PrimaryColumn("Id");

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
