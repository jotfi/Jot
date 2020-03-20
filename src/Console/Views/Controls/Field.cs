using System;
using System.Collections.Generic;
using System.Text;
using Terminal.Gui;

namespace johncocom.Jot.Console.Views.Controls
{
    public class Field
    {
        public string Id { get; }
        public Label Label { get; private set; }
        public string LabelText { get; set; }
        public bool ShowLabel { get; set; } = true;
        public bool AutoAlign { get; set; } = true;
        public (Pos, Pos) LabelPos { get; set; }
        public (Dim, Dim) LabelSize { get; set; }        
        public TextField TextField { get; private set; }
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
            if (TextField == null)
            {
                return string.Empty;
            }
            return TextField.Text.ToString();
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
                    Label.Width = Dim.Sized(maxLength);
                }
                else
                {
                    (Label.X, Label.Y) = LabelPos;
                    (Label.Width, Label.Height) = LabelSize;
                }
            }
            if (ShowTextField)
            {
                views.Add(TextField = new TextField(Text) { Id = Id });
                TextField.Secret = Secret;
                if (AutoAlign)
                {
                    if (Label != null)
                    {
                        TextField.Y = Pos.Top(Label);
                        TextField.X = Pos.Right(Label);
                    }
                    else if (previous != null)
                    {
                        TextField.Y = Pos.Top(previous);
                        if (previous is TextField)
                        {
                            TextField.X = Pos.Left(previous);
                        }
                        else
                        {
                            TextField.X = Pos.Right(previous);
                        }
                    }
                    TextField.Height = 1;
                    TextField.Width = Dim.Fill();
                }
                else
                {
                    (TextField.X, TextField.Y) = TextPos;
                    (TextField.Width, TextField.Height) = TextSize;
                }
            }
        }
    }
}
