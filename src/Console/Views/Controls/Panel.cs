using jotfi.Jot.Base.System;
using jotfi.Jot.Console.Classes;
using jotfi.Jot.Console.Views.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terminal.Gui;

namespace jotfi.Jot.Console.Views.Controls
{
    public class Panel : BaseControl
    {
        public ConsoleApplication App { get; }
        public string Id { get; }
        public string Title { get; set; } = "";
        public (int, int) Pos { get; set; } = (1, 1);
        public (int, int) Size { get; set; } = (80, 24);
        public List<Field> Fields { get; } = new List<Field>();
        public List<View> Views { get; } = new List<View>();

        public Panel(ConsoleApplication app, string id, LogOpts opts = null) : base(opts)
        {
            App = app;
            Id = id;
        }

        public bool ShowDialog(string id = "", bool showCancel = true, (string, string) buttonCaptions = default)
        {
            if (Fields.Count() == 0)
            {
                return false;
            }
            var views = GetViews();
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

        public void ShowPanel(string id = "")
        {
            if (Fields.Count() == 0)
            {
                return;
            }
            var window = new FrameView(Title);
            (window.X, window.Y) = Pos.ToPos();
            (window.Width, window.Height) = Size.ToDim();
            var views = GetViews();
            window.Add(views.ToArray());
            window.Add(Views.ToArray());
            if (!string.IsNullOrEmpty(id))
            {
                window.SetFocus(views.Find(p => p.Id == id));
            }
            App.AddMain(window);
        }

        protected List<View> GetViews()
        {
            int maxLength = 0;
            foreach (var field in Fields)
            {
                if (field.ShowTextField && field.ViewText.Length > maxLength)
                {
                    maxLength = field.ViewText.Length;
                }
            }
            Field previous = null;
            var views = new List<View>();
            foreach (var field in Fields)
            {
                field.Create(views, previous, maxLength);
                previous = field;
            }
            return views;
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
