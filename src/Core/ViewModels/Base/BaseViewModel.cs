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
        public Application App;
        public Settings.AppSettings AppSettings { get => App.AppSettings; }
        public DatabaseController Database { get => App.Database; }
        public RepositoryFactory Repository { get => App.Repository; }
        public ViewModelFactory ViewModels { get => App.ViewModels; }

        public BaseViewModel(Application app, LogOpts opts = null) : base(opts)
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
