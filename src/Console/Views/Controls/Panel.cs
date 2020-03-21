using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terminal.Gui;

namespace jotfi.Jot.Console.Views.Controls
{
    public class Panel : BaseControl
    {
        public string Id { get; }
        public string Title { get; private set; } = "";
        public (int, int) Size { get; set; } = (80, 24);
        public List<Field> Fields { get; } = new List<Field>();

        public Panel(string id)
        {
            Id = id;
        }

        public bool ShowDialog(string okCaption = "Ok", string cancelCaption = "Cancel")
        {
            if (Fields.Count() == 0)
            {
                return false;
            }
            var views = new List<View>();
            int maxLength = 0;
            foreach (var field in Fields.Where(p => p.AutoAlign))
            {
                if (field.LabelText.Length > maxLength)
                {
                    maxLength = field.LabelText.Length;
                }
            }
            View previous = null;
            foreach (var field in Fields)
            {
                field.Create(views, previous, maxLength);
                previous = (View)field.Label ?? field.TextBox;
            }
            var dialog = new Dialog(Title, Size.Item1, Size.Item2)
            {
                views.ToArray()
            };
            dialog.AddButton(GetOkButton(okCaption));
            dialog.AddButton(GetCancelButton(cancelCaption));
            Application.Run(dialog);
            return OkClicked;
        }


        public void SetTitle(string title)
        {
            Title = title;
        }

        public string GetText(string id)
        {
            if (!Fields.Any(p => p.Id == id))
            {
                return string.Empty;
            }
            return Fields.Find(p => p.Id == id).GetText();
        }

        public void SetText(string id, string text)
        {
            if (!Fields.Any(p => p.Id == id))
            {
                return;
            }
            Fields.Find(p => p.Id == id).SetText(text);
        }

        public void SetLabel(string id, string text)
        {
            if (!Fields.Any(p => p.Id == id))
            {
                return;
            }
            Fields.Find(p => p.Id == id).SetLabel(text);
        }

        public void SetColor(string id, ColorScheme color)
        {
            if (!Fields.Any(p => p.Id == id))
            {
                return;
            }
            Fields.Find(p => p.Id == id).SetColor(color);
        }
    }
}
