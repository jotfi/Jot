using System;
using System.Collections.Generic;
using System.Text;
using Terminal.Gui;

namespace johncocom.Jot.Console.Views.Controls
{
    public class Field : Label
    {
        public TextField TextField { get; }
        public bool ShowLabel { get; set; } = true;
        public bool ShowTextField { get; set; } = true;
        public bool AutoAlign { get; set; } = true;

        public Field(string labelText = "", string fieldText = "", 
            int x = 1, int y = 1, bool secret = false) : base(x, y, labelText)
        {
            TextField = new TextField(fieldText)
            {
                X = Pos.Top(this),
                Y = Pos.Right(this),
                Height = 1,
                Width = Dim.Fill(),
                Secret = secret,
            };
        }
    }
}
