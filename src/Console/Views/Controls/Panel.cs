﻿#region License
//
// Copyright (c) 2020, John Cottrell <me@john.co.com>
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
//
#endregion
using jotfi.Jot.Base.System;
using jotfi.Jot.Console.Classes;
using jotfi.Jot.Console.Views.Base;
using jotfi.Jot.Console.Views.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terminal.Gui;

namespace jotfi.Jot.Console.Views.Controls
{
    public class Panel : BaseControl
    {
        public string Id { get; }
        public string Title { get; set; } = "";
        public (int, int) Pos { get; set; } = (1, 1);
        public (int, int) Size { get; set; } = (80, 24);
        public List<Field> Fields { get; } = new List<Field>();
        public List<View> Views { get; } = new List<View>();

        public Panel(string id)
        {
            Id = id;
        }

        public bool ShowDialog(string id = "", bool showCancel = true, (string, string) buttonCaptions = default)
        {
            if (Fields.Count() == 0)
            {
                return false;
            }
            var views = GetViews();
            var dialog = new Dialog(Title, Size.Item1, Size.Item2)
            {
                views.ToArray()
            };
            if (!string.IsNullOrEmpty(id))
            {
                dialog.SetFocus(views.Find(p => p.Id == id));
            }
            dialog.AddButton(GetOkButton(buttonCaptions.Item1));
            if (showCancel)
            {
                dialog.AddButton(GetCancelButton(buttonCaptions.Item2));
            }
            Application.Run(dialog);
            return OkClicked;
        }

        public FrameView GetFrameView(string id = "")
        {
            var window = new FrameView(Title);
            (window.X, window.Y) = Pos.ToPos();
            (window.Width, window.Height) = Size.ToDim();
            var views = GetViews();
            window.Add(views.ToArray());
            window.Add(Views.ToArray());
            if (!string.IsNullOrEmpty(id))
            {
                window.SetFocus(views.Find(p => p.Id == id));
            }
            return window;
        }

        protected List<View> GetViews()
        {
            int maxLength = 0;
            foreach (var field in Fields)
            {
                if (field.ShowTextField && field.ViewText.Length > maxLength)
                {
                    maxLength = field.ViewText.Length;
                }
            }
            Field previous = null;
            var views = new List<View>();
            foreach (var field in Fields)
            {
                field.Create(views, previous, maxLength);
                previous = field;
            }
            return views;
        }

        public string GetText(string id)
        {
            if (!Fields.Any(p => p.Id == id))
            {
                return string.Empty;
            }
            return Fields.Find(p => p.Id == id).GetText();
        }

        public void SetText(string id, string text)
        {
            if (!Fields.Any(p => p.Id == id))
            {
                return;
            }
            Fields.Find(p => p.Id == id).SetText(text);
        }

        public void SetLabel(string id, string text)
        {
            if (!Fields.Any(p => p.Id == id))
            {
                return;
            }
            Fields.Find(p => p.Id == id).SetViewText(text);
        }

        public void SetColor(string id, ColorScheme color)
        {
            if (!Fields.Any(p => p.Id == id))
            {
                return;
            }
            Fields.Find(p => p.Id == id).SetColor(color);
        }
    }
}
