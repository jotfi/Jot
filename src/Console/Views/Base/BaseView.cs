using johncocom.Jot.Base.System;
using johncocom.Jot.Console.Views.Controls;
using johncocom.Jot.Core.ViewModels.Base;
using johncocom.Jot.Core.Views.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terminal.Gui;

namespace johncocom.Jot.Console.Views.Base
{
    public abstract class BaseView : Logger, IBaseView
    {
        private readonly ConsoleApplication App;
        private readonly BaseViewModel ViewModel;

        private List<Panel> Panels { get; } = new List<Panel>();

        public BaseView(ConsoleApplication app, BaseViewModel vm, LogOpts opts = null) : base(opts)
        {
            App = app;
            ViewModel = vm;
        }

        protected virtual void AddToTop(View view)
        {
            Application.Top.Add(view);
        }

        protected virtual void AddToPanel(Field field, string panel = "main")
        {
            GetPanel(panel).Fields.Add(field);
        }

        protected virtual void SetPanelTitle(string title, string panel = "main")
        {
            GetPanel(panel).SetTitle(title);
        }

        protected virtual string GetPanelText(string id, string panel = "main")
        {
            return GetPanel(panel).GetText(id);
        }

        protected virtual void SetPanelText(string id, string text, string panel = "main")
        {
            GetPanel(panel).SetText(id, text);
        }

        protected virtual void SetPanelLabel(string id, string text, string panel = "main")
        {
            GetPanel(panel).SetLabel(id, text);
        }

        protected virtual bool ShowPanelDialog(string panel = "main")
        {
            return GetPanel(panel).ShowDialog();
        }

        protected Panel GetPanel(string panel)
        {
            if (!Panels.Any(p => p.Id == panel))
            {
                var newPanel = new Panel(panel);
                Panels.Add(newPanel);
                return newPanel;
            }
            return Panels.Find(p => p.Id == panel);
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
