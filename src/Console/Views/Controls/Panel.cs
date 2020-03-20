using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terminal.Gui;

namespace johncocom.Jot.Console.Views.Controls
{
    public class Panel
    {
        public string Id { get; }
        public List<Field> Fields { get; } = new List<Field>();

        public Panel(string id)
        {
            Id = id;
        }

        public View[] GetViews()
        {
            var views = new List<View>();
            if (Fields.Count == 0)
            {
                return views.ToArray();
            }
            var maxLabel = new Label("");
            foreach (var field in Fields.Where(p => p.AutoAlign))
            {
                if (field.Text.Length > maxLabel.Text.Length)
                {
                    maxLabel = field;
                }
            }
            var currentField = Fields[0];
            foreach (var field in Fields)
            {
                if (field.ShowLabel)
                {
                    views.Add(field);
                    if (field.AutoAlign)
                    {
                        if (field != currentField)
                        {
                            field.Y = Pos.Bottom(currentField) + 1;
                        }                        
                    }
                }
                if (field.ShowTextField)
                {
                    views.Add(field.TextField);
                    if (field.AutoAlign)
                    {
                        field.TextField.Y = Pos.Right(maxLabel);
                    }
                }                
            }
            return views.ToArray();
        }
    }
}
