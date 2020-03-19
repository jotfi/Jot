using Nista.Jottre.Base.System;
using Nista.Jottre.Core.Views;
using Nista.Jottre.Core.Views.Base;
using Nista.Jottre.Data;
using Nista.Jottre.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Nista.Jottre.Core.ViewModels.Base
{
    public class BaseViewModel : Logger, INotifyPropertyChanged
    {
        private readonly Application App;
        private IBaseView View;

        public BaseViewModel(Application app, LogOpts opts = null) : base(opts)
        {
            App = app;            
        }

        public void SetView(IBaseView view)
        {
            View = view;
        }

        protected Application GetApp()
        {
            return App;
        }

        protected DatabaseController GetDatabase()
        {
            return App.Database;
        }

        protected RepositoryController GetRepository()
        {
            return App.Repository;
        }

        protected ViewModelController GetViewModels()
        {
            return App.ViewModels;
        }

        protected ViewController GetViews()
        {
            return App.Views;
        }

        protected IBaseView GetView()
        {
            return View;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
