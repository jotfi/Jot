using Nista.Jottre.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nista.Jottre.Model
{
    public class Model
    {
        public static List<string> CreateTables { get; }

        static Model()
        {
            CreateTables = new List<string>()
            {
                System.Organization.CreateTable(),
                System.Person.CreateTable(),
                System.User.CreateTable(),
            };
        }
    }
}
