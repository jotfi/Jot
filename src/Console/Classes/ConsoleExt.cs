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
            return source > 0 ? source : 1;
        }

        public static (Pos, Pos) ToPos(this (int, int) source)
        {
            return (source.Item1.ToPos(), source.Item2.ToPos());
        }

        public static Dim ToDim(this int source)
        {
            return source > 0 ? source : Dim.Fill();
        }

        public static (Dim, Dim) ToDim(this (int, int) source)
        {
            return (source.Item1.ToDim(), source.Item2.ToDim());
        }
    }
}
