using jotfi.Jot.Base.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using Terminal.Gui;

namespace jotfi.Jot.Console.Views.Controls
{
    public class TextBox : TextField
    {
        public Action<string> TextChanged { get; set; }
        private readonly object Model;
        public TextBox(string text, object model) : base(text)
        {
            Model = model;
        }

        public override bool ProcessKey(KeyEvent kb)
        {
            var processKey = base.ProcessKey(kb);
            TextChanged?.Invoke(Text.ToString());
            FieldUtils.SetValue(Model, Id.ToString(), Text.ToString());
            return processKey;
        }
    }
}
