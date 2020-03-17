using Dapper;
using Nista.Jottre.Base;
using Nista.Jottre.Database.Base;
using Nista.Jottre.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Nista.Jottre.Database
{
    //TODO: potentially abstract Database to not include Dapper, could use MongoDB in future, etc.
    
    public class DatabaseController : Logger
    {
        private readonly UnitOfWorkContext Context;
        private readonly List<string> CreateTables;
        
        public DatabaseController(UnitOfWorkContext context)
        {
            Context = context;
            CreateTables = new List<string>()
            {
                Model.System.Organization.CreateTable(),
                Model.System.Person.CreateTable(),
                Model.System.User.CreateTable(),
            };
        }

        public void Setup()
        {
            using (Context.Create())
            {
                foreach (var table in CreateTables)
                {
                    try
                    {
                        Context.GetConnection().Execute(table);
                    }
                    catch (Exception ex)
                    {
                        Log(ex, table);
                    }
                }
            }
        }
    }
}
