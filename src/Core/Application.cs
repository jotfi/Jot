using jotfi.Jot.Base.Settings;
using jotfi.Jot.Base.System;
using jotfi.Jot.Core.Settings;
using jotfi.Jot.Core.ViewModels;
using jotfi.Jot.Core.Views;
using jotfi.Jot.Data;
using jotfi.Jot.Database;
using System;
using System.Net.Http;

namespace jotfi.Jot.Core
{
    public class Application : Logger
    {
        public readonly AppSettings AppSettings;
        public readonly DatabaseController Database;
        public readonly RepositoryFactory Repository;
        public readonly ViewModelFactory ViewModels;
        public IViewFactory Views { get; protected set; }
        public HttpClient Client { get; } = new HttpClient();

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

        public virtual void Run()
        {

        }

        public virtual void Quit() { }
        public virtual void ShowError(string message) { }
        public virtual void SaveSettings() => SettingsUtils.SaveSettings(AppSettings);

        public virtual bool IsLoggedIn()
        {
            return false;
        }

    }
}
