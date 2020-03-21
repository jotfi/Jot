﻿using jotfi.Jot.Base.System;
using jotfi.Jot.Console.Views.Controls;
using jotfi.Jot.Core.ViewModels;
using jotfi.Jot.Core.ViewModels.Base;
using jotfi.Jot.Core.Views.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terminal.Gui;

namespace jotfi.Jot.Console.Views.Base
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

        protected virtual void ClearPanel(string panelId = "main")
        {
            GetPanel(panelId).Fields.Clear();
        }

        protected virtual void AddToPanel(Field field, string panelId = "main")
        {
            GetPanel(panelId).Fields.Add(field);
        }

        protected virtual void SetPanelTitle(string title, string panelId = "main")
        {
            GetPanel(panelId).SetTitle(title);
        }

        protected virtual string GetPanelText(string id, string panelId = "main")
        {
            return GetPanel(panelId).GetText(id);
        }

        protected virtual void SetPanelText(string id, string text, string panelId = "main")
        {
            GetPanel(panelId).SetText(id, text);
        }

        protected virtual void SetPanelLabel(string id, string text, string panelId = "main")
        {
            GetPanel(panelId).SetLabel(id, text);
        }

        protected virtual void SetPanelColor(string id, ColorScheme color, string panelId = "main")
        {
            GetPanel(panelId).SetColor(id, color);
        }

        protected virtual bool ShowPanelDialog(string panelId = "main", string okCaption = "Ok", string cancelCaption = "Cancel")
        {
            return GetPanel(panelId).ShowDialog(okCaption, cancelCaption);
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

        public ViewModelController GetViewModels()
        {
            return App.ViewModels;
        }

        public BaseViewModel GetViewModel()
        {
            return ViewModel;
        }
    }
}
