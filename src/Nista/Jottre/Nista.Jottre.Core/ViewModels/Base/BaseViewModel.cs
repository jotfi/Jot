using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Nista.Jottre.Core.ViewModels.Base
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected readonly ViewModelController ViewModels;
        protected readonly Application App;

        public BaseViewModel(ViewModelController viewmodels)
        {
            ViewModels = viewmodels;
            App = viewmodels.App;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
