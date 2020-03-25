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

        public bool ShowDialog(string id = "", bool showCancel = true, (string, string) buttonCaptions = default)
        {
            if (Fields.Count() == 0)
            {
                return false;
            }
            var views = new List<View>();
            int maxLength = 0;
            foreach (var field in Fields)
            {
                if (field.ShowTextField && field.ViewText.Length > maxLength)
                {
                    maxLength = field.ViewText.Length;
                }
            }
            Field previous = null;
            foreach (var field in Fields)
            {
                field.Create(views, previous, maxLength);
                previous = field;
            }
            var dialog = new Dialog(Title, Size.Item1, Size.Item2)
            {
                views.ToArray()
            };
            if (!string.IsNullOrEmpty(id))
            {
                dialog.SetFocus(views.Find(p => p.Id == id));
            }
            dialog.AddButton(GetOkButton(buttonCaptions.Item1));
            if (showCancel)
            {
                dialog.AddButton(GetCancelButton(buttonCaptions.Item2));
            }
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
            Fields.Find(p => p.Id == id).SetViewText(text);
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
