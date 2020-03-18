using Nista.Jottre.Base.Log;
using Nista.Jottre.Data;
using Nista.Jottre.Database;
using System;

namespace Nista.Jottre.Core
{
    public class Application : Logging
    {
        public readonly DatabaseController Database;
        public readonly RepositoryController Repository;
        public readonly ViewModelController ViewModels;
        public ViewController Views { get; protected set; }
        public bool IsInit { get; private set; } = false;

        public Application(bool isConsole) : base(isConsole)
        {
            try
            {
                Database = new DatabaseController(isConsole, MessageBox);
                Repository = new RepositoryController(Database, isConsole, MessageBox);
                ViewModels = new ViewModelController(this);                
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        public override void InitLog()
        {
            Logger = new Logger(this, IsConsole, MessageBox);
        }

        public virtual void MessageBox(string message)
        {

        }

        public void Init()
        {
            try
            {
                foreach (var view in Views.Items)
                {
                    if (view == null)
                    {
                        throw new NotImplementedException();
                    }
                }
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
