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
        public string Title { get; private set; } = "";
        public (int, int) Size { get; set; } = (80, 24);
        public List<Field> Fields { get; } = new List<Field>();

        public Panel(string id)
        {
            Id = id;
        }

        public bool ShowDialog()
        {
            var cancel = false;
            var saveButton = new Button(3, 14, "Save")
            {
                Clicked = () => Application.RequestStop()
            };
            var cancelButton = new Button(10, 14, "Cancel")
            {
                Clicked = () => { Application.RequestStop(); cancel = true; }
            };
            var dialog = new Dialog(Title, Size.Item1, Size.Item2, saveButton, cancelButton);
            dialog.Add(GetViews());
            Application.Run(dialog);
            return cancel;
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

        public View[] GetViews()
        {
            var views = new List<View>();
            if (Fields.Count == 0)
            {
                return views.ToArray();
            }
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
                previous = (View)field.Label ?? field.TextField;
            }
            return views.ToArray();
        }
    }
}
