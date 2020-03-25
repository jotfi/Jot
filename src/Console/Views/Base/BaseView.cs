using jotfi.Jot.Base.System;
using jotfi.Jot.Console.Views.Controls;
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
        private readonly ConsoleApplication App;
        private readonly T ViewModel;

        private List<Panel> Panels { get; } = new List<Panel>();

        public BaseView(ConsoleApplication app, T viewmodel, LogOpts opts = null) 
            : base(opts)
        {
            App = app;
            ViewModel = viewmodel;
        }

        public Core.Application GetApp() => App;
        public Core.Settings.AppSettings GetAppSettings() => GetApp().AppSettings;
        public T GetViewModel() => ViewModel;
        public ConsoleApplication GetConsoleApp() => App;        
        public ViewModelFactory GetViewModels() => App.ViewModels;
        public Mono.Terminal.MainLoop GetMainLoop() => Terminal.Gui.Application.MainLoop;
        public Terminal.Gui.Dim GetFill() => Terminal.Gui.Dim.Fill();
        public Terminal.Gui.ColorScheme GetMenuColor() => Terminal.Gui.Colors.Menu;
        public Terminal.Gui.ColorScheme GetErrorColor() => Terminal.Gui.Colors.Error;

        public virtual void Quit()
        {
            if (App.Quit())
            {
                Terminal.Gui.Application.Top.Running = false;
            }
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

        protected virtual bool ShowPanelDialog(string id = "", string panelId = "main", 
            string okCaption = "Ok", string cancelCaption = "Cancel")
        {
            return GetPanel(panelId).ShowDialog(id, okCaption, cancelCaption);
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
