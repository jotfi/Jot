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
using jotfi.Jot.Base.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace jotfi.Jot.Base.Utils
{
    public class FieldUtils
    {
        public static void SetValue(object model, string id, string value)
        {
            var property = model.GetType().GetProperty(id);
            switch (property?.PropertyType?.FullName?.ToLower())
            {
                case "system.boolean":
                    property.SetValue(model, value.ToBool());
                    break;
                case "system.decimal":
                    property.SetValue(model, value.ToDec());
                    break;
                case "system.int32":
                    property.SetValue(model, value.ToInt());
                    break;
                case "system.datetime":
                    property.SetValue(model, value.ToDateTime());
                    break;
                default:
                    property?.SetValue(model, value);
                    break;
            }
        }
    }
}
