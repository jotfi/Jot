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
        public Label Label { get; private set; }
        public string LabelText { get; set; } = "";
        public ColorScheme ColorScheme { get; set; }
        public bool ShowLabel { get; set; } = true;
        public bool AutoAlign { get; set; } = true;
        public bool AutoSize{ get; set; } = true;
        public (Pos, Pos) LabelPos { get; set; } =  (1, 1);
        public (Dim, Dim) LabelSize { get; set; } = (Dim.Fill(), 1);
        public TextBox TextBox { get; private set; }
        public Action<string> TextChanged { get; set; }
        public string Text { get; set; } = "";
        public bool ShowTextField { get; set; } = true;
        public (Pos, Pos) TextPos { get; set; } = (1, 1);
        public (Dim, Dim) TextSize { get; set; } = (Dim.Fill(), 1);
        public bool Secret { get; set; } = false;

        public Field(string id)
        {
            Id = id;
        }

        public Field(string id, object model) : this(id)
        {
            Model = model;
            var property = model?.GetType().GetProperty(id);
            LabelText = property?.GetCustomAttribute<DisplayAttribute>()?.Name ?? "";
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

        public void SetLabel(string text)
        {
            if (Label == null)
            {
                return;
            }
            Label.Text = text;
        }

        public void SetColor(ColorScheme color)
        {
            if (Label == null)
            {
                return;
            }
            Label.ColorScheme = color;
        }

        public void Create(List<View> views, View previous, int maxLength)
        {
            if (ShowLabel)
            {
                views.Add(Label = new Label(LabelText) { Id = Id + "Label" });
                Label.ColorScheme = ColorScheme;
                if (AutoAlign && previous != null)
                {
                    LabelPos = (Pos.Left(previous), Pos.Bottom(previous) + 1);
                }
                if (AutoSize)
                {
                    LabelSize = (Dim.Sized(maxLength), 1);
                }                
                (Label.X, Label.Y) = LabelPos;
                (Label.Width, Label.Height) = LabelSize;
            }
            if (ShowTextField)
            {
                views.Add(TextBox = new TextBox(Text, Model) { Id = Id });
                TextBox.Secret = Secret;
                TextBox.TextChanged = TextChanged;
                if (AutoAlign)
                {
                    if (Label != null)
                    {
                        TextPos = (Pos.Right(Label) + 2, Pos.Top(Label));
                    }
                    else if (previous != null)
                    {
                        var xPos = (previous is TextField) ? Pos.Left(previous) : Pos.Right(previous);
                        TextPos = (xPos, Pos.Top(previous));                        
                    }
                }
                (TextBox.X, TextBox.Y) = TextPos;
                (TextBox.Width, TextBox.Height) = TextSize;                
            }
        }
    }
}
