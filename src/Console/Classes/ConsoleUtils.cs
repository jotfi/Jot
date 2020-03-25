using System;
using System.Collections.Generic;
using System.Text;

namespace jotfi.Jot.Console.Classes
{
    public class ConsoleUtils
    {        
        const string SelectionMarked = "[x] ";
        const string SelectionNotMarked = "[ ] ";

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
