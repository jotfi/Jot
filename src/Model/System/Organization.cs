﻿using johncocom.Jot.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace johncocom.Jot.Model.System
{
    public class Organization : Entity
    {
        public string Name { get; set; }
        public bool CanLogin { get; set; }
        public List<Person> Contacts { get; set; }

        public override string CreateTable()
        {
            return $@"
create table {TableName()}(
{TransactionFields()}, 
{EntityFields()}, 
Name text not null,
CanLogin integer);";
        }
    }
}