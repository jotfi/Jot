using jotfi.Jot.Base.System;
using jotfi.Jot.Console.Views.Controls;
using jotfi.Jot.Core.Settings;
using jotfi.Jot.Core.ViewModels;
using jotfi.Jot.Core.ViewModels.Base;
using jotfi.Jot.Core.Views.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jotfi.Jot.Console.Views.Base
{
    public abstract class BaseView<T> : Logger, IBaseView<T>
    {
        public Core.Application App { get; }
        public T ViewModel { get; }
        public ConsoleApplication ConsoleApp => (ConsoleApplication)App;
        public ViewModelFactory ViewModels => App.ViewModels;
        public AppSettings AppSettings => App.AppSettings;
        public Mono.Terminal.MainLoop MainLoop => Terminal.Gui.Application.MainLoop;
        public Terminal.Gui.Dim DimFill => Terminal.Gui.Dim.Fill();
        public Terminal.Gui.ColorScheme MenuColor => Terminal.Gui.Colors.Menu;
        public Terminal.Gui.ColorScheme ErrorColor => Terminal.Gui.Colors.Error;

        private List<Panel> Panels { get; } = new List<Panel>();

        public BaseView(Core.Application app, T viewmodel, LogOpts opts = null) 
            : base(opts)
        {
            App = app;
            ViewModel = viewmodel;
        }

        protected virtual void AddToTop(Terminal.Gui.View view) => Terminal.Gui.Application.Top.Add(view);
        protected virtual void ClearPanel(string panelId = "main") => GetPanel(panelId).Fields.Clear();
        protected virtual void AddToPanel(Field field, 
            string panelId = "main") => GetPanel(panelId).Fields.Add(field);
        protected virtual void SetPanelTitle(string title, 
            string panelId = "main") => GetPanel(panelId).SetTitle(title);        
        protected virtual string GetPanelText(string id, 
            string panelId = "main") => GetPanel(panelId).GetText(id);
        protected virtual void SetPanelText(string id, string text, 
            string panelId = "main") => GetPanel(panelId).SetText(id, text);
        protected virtual void SetPanelLabel(string id, string text, 
            string panelId = "main") => GetPanel(panelId).SetLabel(id, text);
        protected virtual void SetPanelColor(string id, Terminal.Gui.ColorScheme color, 
            string panelId = "main") => GetPanel(panelId).SetColor(id, color);

        protected virtual bool ShowPanelDialog(string id = "", bool showCancel = true,
            string panelId = "main", string okCaption = "Ok", string cancelCaption = "Cancel")
        {
            return GetPanel(panelId).ShowDialog(id, showCancel, (okCaption, cancelCaption));
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
    }
}
