using johncocom.Jot.Base.System;
using johncocom.Jot.Core.ViewModels;
using johncocom.Jot.Core.Views;
using johncocom.Jot.Data;
using johncocom.Jot.Database;
using System;

namespace johncocom.Jot.Core
{
    public class Application : Logger
    {
        public readonly DatabaseController Database;
        public readonly RepositoryController Repository;
        public readonly ViewModelController ViewModels;
        public ViewController Views { get; protected set; }
        public bool IsInit { get; private set; } = false;

        public Application(bool isConsole) : base()
        {
            try
            {
                Opts = new LogOpts(isConsole, ShowError);
                Database = new DatabaseController(Opts);
                Repository = new RepositoryController(Database, Opts);
                ViewModels = new ViewModelController(this, Opts);
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        public virtual void ShowError(string message)
        {

        }

        public void Init()
        {
            try
            {                
                ViewModels.SetupViews();
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
                ViewModels.Start.Run();
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

        public virtual bool IsLoggedIn()
        {
            return false;
        }

    }
}
