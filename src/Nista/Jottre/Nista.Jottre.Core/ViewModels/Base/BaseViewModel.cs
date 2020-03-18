using Nista.Jottre.Base.System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Nista.Jottre.Core.ViewModels.Base
{
    public class BaseViewModel : Logger, INotifyPropertyChanged
    {
        protected readonly Application Application;

        public BaseViewModel(Application app, LogOpts opts = null) : base(opts)
        {
            Application = app;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
