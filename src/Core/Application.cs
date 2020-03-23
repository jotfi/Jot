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
        public readonly RepositoryController Repository;
        public readonly ViewModelController ViewModels;
        public ViewController Views { get; protected set; }
        public bool IsInit { get; private set; } = false;

        public Application(AppSettings appSettings) : base()
        {
            try
            {
                AppSettings = appSettings;
                Opts = new LogOpts(appSettings.IsConsole, ShowError);
                Database = new DatabaseController(appSettings, Opts);
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
