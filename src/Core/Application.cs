// Copyright 2020 John Cottrell
//
// This file is part of Jot.
//
// Jot is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Jot is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with Jot.  If not, see <https://www.gnu.org/licenses/>.

using jotfi.Jot.Base.Settings;
using jotfi.Jot.Base.System;
using jotfi.Jot.Core.Settings;
using jotfi.Jot.Core.Services;
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
        public readonly DatabaseService Database;
        public readonly RepositoryFactory Repository;
        public readonly ServiceFactory Services;
        public IViewFactory Views { get; protected set; }
        public HttpClient Client { get; } = new HttpClient();

        public Application(AppSettings appSettings) : base()
        {
            try
            {
                AppSettings = appSettings;
                Opts = new LogOpts(appSettings.IsConsole, ShowError);
                Database = new DatabaseService(this, Opts);
                Repository = new RepositoryFactory(Database.Context, Opts);
                Services = new ServiceFactory(this, Opts);
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
