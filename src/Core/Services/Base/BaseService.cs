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

using jotfi.Jot.Base.System;
using jotfi.Jot.Core.Views;
using jotfi.Jot.Core.Views.Base;
using jotfi.Jot.Data;
using jotfi.Jot.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace jotfi.Jot.Core.Services.Base
{
    public class BaseService : Logger, INotifyPropertyChanged
    {
        public Application App;
        public Settings.AppSettings AppSettings { get => App.AppSettings; }
        public DatabaseService Database { get => App.Database; }
        public RepositoryFactory Repository { get => App.Repository; }
        public ServiceFactory Services { get => App.Services; }

        public BaseService(Application app, LogOpts opts = null) : base(opts)
        {
            App = app;            
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
