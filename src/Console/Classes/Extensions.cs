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
using System;
using System.Collections.Generic;
using System.Text;
using Terminal.Gui;

namespace jotfi.Jot.Console.Classes
{
    public static class Extensions
    {
        public static Pos ToPos(this int source)
        {
            return Math.Abs(source);
        }

        public static (Pos, Pos) ToPos(this (int, int) source)
        {
            return (source.Item1.ToPos(), source.Item2.ToPos());
        }

        public static Dim ToDim(this int source)
        {
            if (source > 0)
            {
                return source;
            }
            return Dim.Fill() + source;
        }

        public static (Dim, Dim) ToDim(this (int, int) source)
        {
            return (source.Item1.ToDim(), source.Item2.ToDim());
        }

        public static string ToCheckMark(this bool source)
        {
            return source ? ConsoleUtils.SelectionMarked : ConsoleUtils.SelectionNotMarked;
        }
    }
}
