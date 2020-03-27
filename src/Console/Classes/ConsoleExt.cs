using System;
using System.Collections.Generic;
using System.Text;
using Terminal.Gui;

namespace jotfi.Jot.Console.Classes
{
    public static class ConsoleExt
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
