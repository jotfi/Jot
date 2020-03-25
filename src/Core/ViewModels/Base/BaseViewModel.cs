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

namespace jotfi.Jot.Core.ViewModels.Base
{
    public class BaseViewModel : Logger, INotifyPropertyChanged
    {
        private readonly Application App;

        public BaseViewModel(Application app, LogOpts opts = null) : base(opts)
        {
            App = app;            
        }

        protected Application GetApp() => App;
        public Settings.AppSettings GetAppSettings() => GetApp().AppSettings;
        protected DatabaseController GetDatabase() => App.Database;
        protected RepositoryFactory GetRepository() => App.Repository;
        protected ViewModelFactory GetViewModels() => App.ViewModels;
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
