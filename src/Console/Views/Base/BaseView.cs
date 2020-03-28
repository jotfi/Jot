// Copyright 2020 John Cottrell
//
// This file is part of Jot.
//
// Jot is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Jot is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with Jot.  If not, see <https://www.gnu.org/licenses/>.

using jotfi.Jot.Base.System;
using jotfi.Jot.Console.Views.Controls;
using jotfi.Jot.Core.Settings;
using jotfi.Jot.Core.Services;
using jotfi.Jot.Core.Services.Base;
using jotfi.Jot.Core.Views.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jotfi.Jot.Console.Views.Base
{
    public abstract class BaseView<T> : BaseControl, IBaseView<T>
    {
        public Core.Application App { get; }
        public T Service { get; }
        public ConsoleApplication ConsoleApp => (ConsoleApplication)App;
        public ServiceFactory Services => App.Services;
        public AppSettings AppSettings => App.AppSettings;
        public Mono.Terminal.MainLoop MainLoop => Terminal.Gui.Application.MainLoop;
        public Terminal.Gui.Dim DimFill => Terminal.Gui.Dim.Fill();
        public Terminal.Gui.ColorScheme MenuColor => Terminal.Gui.Colors.Menu;
        public Terminal.Gui.ColorScheme ErrorColor => Terminal.Gui.Colors.Error;

        private List<Panel> Panels { get; } = new List<Panel>();

        public BaseView(Core.Application app, T service, LogOpts opts = null) 
            : base(opts)
        {
            App = app;
            Service = service;
        }

        protected virtual void Reset() => Panels.Clear();
        protected virtual void AddToTop(Terminal.Gui.View view) => Terminal.Gui.Application.Top.Add(view);
        protected virtual void AddToPanel(Field field, 
            string panelId = "main") => GetPanel(panelId).Fields.Add(field);
        protected virtual void AddToPanel(Terminal.Gui.View view,
            string panelId = "main") => GetPanel(panelId).Views.Add(view);
        protected virtual void SetPanelTitle(string title, 
            string panelId = "main") => GetPanel(panelId).Title = title;
        protected virtual void SetPanelPos(int x, int y,
            string panelId = "main") => GetPanel(panelId).Pos = (x, y);
        protected virtual void SetPanelSize(int width, int height,
            string panelId = "main") => GetPanel(panelId).Size = (width, height);
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

        protected virtual void ShowPanel(string id = "", string panelId = "main")
        {
            GetPanel(panelId).ShowPanel(id);
        }

        protected Panel GetPanel(string panelId = "main")
        {
            if (!Panels.Any(p => p.Id == panelId))
            {
                var newPanel = new Panel(ConsoleApp, panelId, Opts);
                Panels.Add(newPanel);
                return newPanel;
            }
            return Panels.Find(p => p.Id == panelId);
        }    
    }
}
