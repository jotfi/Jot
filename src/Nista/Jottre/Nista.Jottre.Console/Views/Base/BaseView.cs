using Nista.Jottre.Base.System;
using Nista.Jottre.Core.ViewModels.Base;
using Nista.Jottre.Core.Views.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Terminal.Gui;

namespace Nista.Jottre.Console.Views.Base
{
    public abstract class BaseView : Logger, IBaseView
    {
        private readonly ConsoleApplication App;
        private readonly BaseViewModel ViewModel;

        public BaseView(ConsoleApplication app, BaseViewModel vm, LogOpts opts = null) : base(opts)
        {
            App = app;
            ViewModel = vm;
        }

        protected virtual void AddToTop(View view)
        {
            Application.Top.Add(view);
        }


        protected virtual void Quit()
        {
            if (App.Quit())
            {
                Application.Top.Running = false;
            }
        }

        public ConsoleApplication GetConsoleApp()
        {
            return App;
        }

        public Core.Application GetApp()
        {
            return App;
        }

        public BaseViewModel GetViewModel()
        {
            return ViewModel;
        }
    }
}
