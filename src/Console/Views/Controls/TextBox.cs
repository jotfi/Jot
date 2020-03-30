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
