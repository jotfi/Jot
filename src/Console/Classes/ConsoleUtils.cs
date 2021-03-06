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
using System;
using System.Collections.Generic;
using System.Text;

namespace jotfi.Jot.Console.Classes
{
    public class ConsoleUtils
    {        
        public const string SelectionMarked = "[x] ";
        public const string SelectionNotMarked = "[ ] ";

        public static void ChangeSelection(List<string> list, int selectedItem = 0)
        {
            for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];
                if (!item.StartsWith(SelectionMarked) && !item.StartsWith(SelectionNotMarked))
                {
                    item = SelectionNotMarked + item;                    
                }
                if (item.StartsWith(SelectionMarked) && i != selectedItem)
                {
                    item = SelectionNotMarked + item.Substring(SelectionMarked.Length);
                }
                if (item.StartsWith(SelectionNotMarked) && i == selectedItem)
                {
                    item = SelectionMarked + item.Substring(SelectionNotMarked.Length);
                }
                list[i] = item;
            }
        }
    }
}
