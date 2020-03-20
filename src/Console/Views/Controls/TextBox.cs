using System;
using System.Collections.Generic;
using System.Text;
using Terminal.Gui;

namespace johncocom.Jot.Console.Views.Controls
{
    public class TextBox : TextField
    {
        public Action<TextBox> TextChanged { get; set; }

        public TextBox(string text) : base(text)
        {

        }

        public override bool ProcessKey(KeyEvent kb)
        {
            var procKey = base.ProcessKey(kb);
            TextChanged?.Invoke(this);
            return procKey;
        }
    }
}
