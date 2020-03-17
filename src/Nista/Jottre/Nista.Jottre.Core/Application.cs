using Nista.Jottre.Base;
using Nista.Jottre.Core.ViewModels.Base;
using Nista.Jottre.Data;
using Nista.Jottre.Database;
using Nista.Jottre.Database.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nista.Jottre.Core
{
    public class Application : Logger
    {
        public readonly UnitOfWorkContext Context;
        public readonly DatabaseController Database;
        public readonly RepositoryController Repository;
        public readonly ViewModelController ViewModels;        

        public Application(bool isConsole) : base(isConsole)
        {
            try
            {
                Context = new UnitOfWorkContext(SimpleCRUD.Dialects.SQLite);
                Database = new DatabaseController(Context);
                Repository = new RepositoryController(Context);
                ViewModels = new ViewModelController(this);
            }
            catch (Exception ex)
            {
                Error(ex.Message);
            }
        }

        public virtual void Run()
        {
            try
            { 
                Database.Setup();
            }
            catch (Exception ex)
            {
                Error(ex.Message);
            }
        }
    }
}
