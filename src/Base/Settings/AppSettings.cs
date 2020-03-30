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
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace jotfi.Jot.Base.Settings
{
    public class AppSettings
    {
        public bool IsClient { get; set; }
        public string Secret { get; set; } = "http://www.jotfi.com/";
        public bool IsConsole { get; set; }
        [Display(Name = "Server URL")]
        public string ServerUrl { get; set; } = "http://localhost:5000";
        public DbSettings Database { get; set; } = new DbSettings();
    }
}
