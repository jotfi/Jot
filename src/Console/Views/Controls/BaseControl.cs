using System;
using System.Collections.Generic;
using System.Text;
using Terminal.Gui;

namespace jotfi.Jot.Console.Views.Controls
{
    public abstract class BaseControl
    {
        protected bool OkClicked { get; private set; }
        public Button GetOkButton(string caption = "Ok")
        {
            OkClicked = false;
            return new Button(caption)
            {
                Clicked = () => { Application.RequestStop(); OkClicked = true; }
            };
        }
        public Button GetCancelButton(string caption = "Cancel")
        {
            return new Button(caption)
            {
                Clicked = () => Application.RequestStop()
            };
        }
    }
}
