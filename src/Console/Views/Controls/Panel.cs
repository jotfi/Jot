using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terminal.Gui;

namespace jotfi.Jot.Console.Views.Controls
{
    public class Panel
    {
        public string Id { get; }
        public string Title { get; private set; } = "";
        public bool Cancel { get; private set; }
        public (int, int) Size { get; set; } = (80, 24);
        public List<Field> Fields { get; } = new List<Field>();

        public Panel(string id)
        {
            Id = id;
        }

        public void ShowDialog(string saveCaption = "Save", string cancelCaption = "Cancel")
        {
            if (Fields.Count() == 0)
            {
                return;
            }
            Cancel = false;
            Application.Run(GetDialog(saveCaption, cancelCaption));
        }

        public Dialog GetDialog(string saveCaption = "Save", string cancelCaption = "Cancel")
        {
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
            View defaultView = null;
            foreach (var field in Fields)
            {
                field.Create(views, previous, maxLength);
                previous = (View)field.Label ?? field.TextBox;
                if (defaultView == null && field.TextBox != null)
                {
                    defaultView = field.TextBox;
                }
            }
            var dialog = new Dialog(Title, Size.Item1, Size.Item2)
            {
                views.ToArray()
            };
            dialog.AddButton(new Button(3, 14, saveCaption)
            {
                Clicked = () => Application.RequestStop()
            });
            dialog.AddButton(new Button(10, 14, cancelCaption)
            {
                Clicked = () => { Application.RequestStop(); Cancel = true; }
            });
            
            if (defaultView != null)
            {
                //dialog.SetFocus(defaultView);
            }
            return dialog;
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
