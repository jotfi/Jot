#region License
//
// Copyright (c) 2020, John Cottrell <me@john.co.com>
//
// This file is part of Jot.
//
// Jot is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Jot is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with Jot.  If not, see <https://www.gnu.org/licenses/>.
//
#endregion
using jotfi.Jot.Base.System;
using System;
using System.Collections.Generic;
using System.Text;
using Terminal.Gui;

namespace jotfi.Jot.Console.Views.Base
{
    public abstract class BaseControl
    {
        protected bool OkClicked { get; private set; }

        public BaseControl()
        {

        }

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
