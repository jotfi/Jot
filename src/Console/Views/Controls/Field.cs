#region License
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
using jotfi.Jot.Console.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using Terminal.Gui;

namespace jotfi.Jot.Console.Views.Controls
{
    public class Field
    {
        public string Id { get; }
        public object Model { get; }
        public View View { get; private set; }
        public string ViewText { get; set; } = "";
        public ColorScheme ViewColor { get; set; }
        public bool ShowView { get; set; } = true;
        public (int, int) ViewPos { get; set; } =  (1, 1);
        public (int, int) ViewSize { get; set; } = (-1, 1);
        public TextBox TextBox { get; private set; }
        public Action<string> TextChanged { get; set; }
        public Action<int> ListChanged { get; set; }
        public string Text { get; set; } = "";
        public bool ShowTextField { get; set; } = true;
        public (int, int) TextPos { get; set; } = (1, 1);
        public (int, int) TextSize { get; set; } = (-1, 1);
        public bool Secret { get; set; } = false;

        public Field(string id)
        {
            Id = id;
        }

        public Field(string id, View view) : this(id)
        {
            View = view;
        }
        
        public Field(string id, object model) : this(id)
        {
            Model = model;
            var property = model?.GetType().GetProperty(id);
            ViewText = property?.GetCustomAttribute<DisplayAttribute>()?.Name ?? "";
            Text = property?.GetValue(model, null)?.ToString() ?? "";
        }

        public string GetText()
        {
            if (TextBox == null)
            {
                return string.Empty;
            }
            return TextBox.Text.ToString();
        }

        public void SetText(string text)
        {
            if (TextBox == null)
            {
                return;
            }
            TextBox.Text = text;
        }

        public void SetViewText(string text)
        {
            if (View == null)
            {
                return;
            }
            if (View is Label label)
            {
                label.Text = text;
            }
        }

        public void SetColor(ColorScheme color)
        {
            if (View == null)
            {
                return;
            }
            View.ColorScheme = color;
        }

        public bool IsAutoAlign() => ViewPos == (1, 1) && TextPos == (1, 1);
        public bool IsAutoSize() => ViewSize == (-1, 1) && TextSize == (-1, 1);

        public void Create(List<View> views, Field previous, int maxLength)
        {
            View previousView = previous?.View ?? previous?.TextBox;
            if (ShowView)
            {
                if (View == null)
                {
                    View = new Label(ViewText) { Id = Id + "Label" };
                    SetColor(ViewColor);
                }                
                else if (View is ListView list)
                {
                    list.SelectedChanged += () => ListChanged?.Invoke(list.SelectedItem);
                }
                var viewPos = ViewPos.ToPos();
                if (IsAutoAlign() && previousView != null)
                {
                    viewPos = (Pos.Left(previousView), Pos.Bottom(previousView) + 1);
                }
                var viewSize = ViewSize.ToDim();
                if (IsAutoSize() && ShowTextField)
                {
                    viewSize = (Dim.Sized(maxLength), 1);
                }                
                (View.X, View.Y) = viewPos;
                (View.Width, View.Height) = viewSize;
                views.Add(View);
            }
            if (ShowTextField)
            {
                views.Add(TextBox = new TextBox(Text, Model) { Id = Id });
                TextBox.Secret = Secret;
                TextBox.TextChanged = TextChanged;
                var textPos = TextPos.ToPos();
                if (IsAutoAlign() && View != null)
                {
                    textPos = (Pos.Right(View) + 2, Pos.Top(View));
                }
                else if (previousView != null)
                {
                    var xPos = (previousView is TextField) ? Pos.Left(previousView) : Pos.Right(previousView);
                    textPos = (xPos, Pos.Top(previousView));
                }
                (TextBox.X, TextBox.Y) = textPos;
                (TextBox.Width, TextBox.Height) = TextSize.ToDim();                
            }
        }
    }
}
