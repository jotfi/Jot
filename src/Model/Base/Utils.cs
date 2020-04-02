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

namespace jotfi.Jot.Model.Base
{
    public class Utils
    {
        public static string GetCodePrefix(string? field1, string? field2 = "")
        {
            var hasBoth = !string.IsNullOrEmpty(field1) && !string.IsNullOrEmpty(field2);
            var fieldLength = hasBoth ? 3 : 6;
            string codePrefix = string.Empty;
            if (!string.IsNullOrEmpty(field1))
            {
                codePrefix += field1.PadRight(fieldLength).Substring(0, fieldLength).Trim();
            }
            if (!string.IsNullOrEmpty(field2))
            {
                codePrefix += field2.PadRight(fieldLength).Substring(0, fieldLength).Trim();
            }
            if (string.IsNullOrEmpty(codePrefix))
            {
                codePrefix = "zblank";
            }
            return codePrefix.ToUpper();
        }
    }
}
