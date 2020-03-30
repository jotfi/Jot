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
using System.Security.Cryptography;
using System.Text;

namespace jotfi.Jot.Base.Utils
{
    public class HashUtils
    {
        public static string GetSHA256Hash(string value)
        {
            byte[] valueBytes = Encoding.Default.GetBytes(value);
            using var SH256Password = SHA256.Create();
            byte[] hashValue = SH256Password.ComputeHash(valueBytes);
            var sb = new StringBuilder();
            foreach (var hashByte in hashValue)
            {
                sb.Append(hashByte.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
