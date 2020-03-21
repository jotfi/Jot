using System;
using System.Collections.Generic;
using System.Text;
using Terminal.Gui;

namespace jotfi.Jot.Console.Views.Controls
{
    public class Field
    {
        public string Id { get; }
        public Label Label { get; private set; }
        public string LabelText { get; set; }
        public bool ShowLabel { get; set; } = true;
        public bool AutoAlign { get; set; } = true;
        public bool AutoSize{ get; set; } = true;
        public (Pos, Pos) LabelPos { get; set; }
        public (Dim, Dim) LabelSize { get; set; }        
        public TextBox TextBox { get; private set; }
        public Action<TextBox> TextChanged { get; set; }
        public string Text { get; set; }
        public bool ShowTextField { get; set; } = true;
        public (Pos, Pos) TextPos { get; set; }
        public (Dim, Dim) TextSize { get; set; }
        public bool Secret { get; set; } = false;        

        public Field(string id, string labelText = "", string text = "")
        {
            Id = id;
            LabelText = labelText;
            Text = text;
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

        public void Create(List<View> views, View previous, int maxLength)
        {
            if (ShowLabel)
            {
                views.Add(Label = new Label(LabelText) { Id = Id + "Label" });
                if (AutoAlign)
                {
                    if (previous != null)
                    {
                        (Label.X, Label.Y) = (Pos.Left(previous), Pos.Bottom(previous) + 1);
                    }
                }
                else
                {
                    (Label.X, Label.Y) = LabelPos;;
                }
                if (AutoSize)
                {
                    Label.Width = Dim.Sized(maxLength);
                }
                else
                {
                    (Label.Width, Label.Height) = LabelSize;
                }
            }
            if (ShowTextField)
            {
                views.Add(TextBox = new TextBox(Text) { Id = Id });
                TextBox.Secret = Secret;
                TextBox.TextChanged = TextChanged;
                if (AutoAlign)
                {
                    if (Label != null)
                    {
                        TextBox.Y = Pos.Top(Label);
                        TextBox.X = Pos.Right(Label);
                    }
                    else if (previous != null)
                    {
                        TextBox.Y = Pos.Top(previous);
                        if (previous is TextField)
                        {
                            TextBox.X = Pos.Left(previous);
                        }
                        else
                        {
                            TextBox.X = Pos.Right(previous);
                        }
                    }
                }
                else
                {
                    (TextBox.X, TextBox.Y) = TextPos;
                }
                if (AutoSize)
                {
                    TextBox.Height = 1;
                    TextBox.Width = Dim.Fill();
                }
                else
                {
                    (TextBox.Width, TextBox.Height) = TextSize;
                }
            }
        }
    }
}
