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
        public readonly DbContext Context;
        public readonly DatabaseController Database;
        public readonly RepositoryController Repository;
        public readonly ViewModelController ViewModels;
        public ViewController Views { get; protected set; }
        public bool IsInit { get; private set; } = false;

        public Application(bool isConsole) : base(isConsole)
        {
            try
            {
                Context = new DbContext(SimpleCRUD.Dialects.SQLite);
                Database = new DatabaseController(Context);
                Repository = new RepositoryController(Context);
                ViewModels = new ViewModelController(this);
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        public void Init(ViewController views)
        {
            try
            {
                Views = views;
                IsInit = true;
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        public virtual void Run()
        {
            try
            {                 
                if (!IsInit)
                {
                    throw new NotImplementedException();
                }
                ViewModels.Login.Run();
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        public virtual bool Quit()
        {
            return true;
        }
    }
}
