using jotfi.Jot.Base.System;
using jotfi.Jot.Core.Settings;
using jotfi.Jot.Core.ViewModels;
using jotfi.Jot.Core.Views;
using jotfi.Jot.Data;
using jotfi.Jot.Database;
using System;

namespace jotfi.Jot.Core
{
    public class Application : Logger
    {
        public readonly AppSettings AppSettings;
        public readonly DatabaseController Database;
        public readonly RepositoryFactory Repository;
        public readonly ViewModelFactory ViewModels;
        public ViewFactory Views { get; protected set; }
        public bool IsInit { get; private set; } = false;

        public Application(AppSettings appSettings) : base()
        {
            try
            {
                AppSettings = appSettings;
                Opts = new LogOpts(appSettings.IsConsole, ShowError);
                Database = new DatabaseController(appSettings, Opts);
                Repository = new RepositoryFactory(Database, Opts);
                ViewModels = new ViewModelFactory(this, Opts);
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
                IsInit = true;
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        public virtual void Run()
        {                 
            if (!IsInit)
            {
                throw new NotImplementedException();
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
