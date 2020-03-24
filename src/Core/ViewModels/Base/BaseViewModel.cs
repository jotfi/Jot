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

        protected Application GetApp()
        {
            return App;
        }

        protected DatabaseController GetDatabase()
        {
            return App.Database;
        }

        protected RepositoryFactory GetRepository()
        {
            return App.Repository;
        }

        protected ViewModelFactory GetViewModels()
        {
            return App.ViewModels;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
