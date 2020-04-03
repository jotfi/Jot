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
using System.Linq;
using System.Text;

namespace jotfi.Jot.Base.System
{
    public static class Assert
    {
        public static void IsEqualTo<T>(this T obj, T other)
        {
            if (obj == null || !obj.Equals(other))
            {
                throw new ApplicationException(string.Format("{0} should be equal to {1}", obj, other));
            }
        }

        public static void IsSequenceEqualTo<T>(this IEnumerable<T> obj, IEnumerable<T> other)
        {
            if (!obj.SequenceEqual(other))
            {
                throw new ApplicationException(string.Format("{0} should be equal to {1}", obj, other));
            }
        }

        public static void IsFalse(this bool b)
        {
            if (b)
            {
                throw new ApplicationException("Expected false");
            }
        }

        public static void IsTrue(this bool b)
        {
            if (!b)
            {
                throw new ApplicationException("Expected true");
            }
        }

        public static void IsNull(this object obj)
        {
            if (obj != null)
            {
                throw new ApplicationException("Expected null");
            }
        }

        public static void IsNotZero(this object obj)
        {
            if (obj.ToInt() < 1)
            {
                throw new ApplicationException("Expected non zero");
            }
        }

        public static void IsNotNull(this object obj)
        {
            if (obj == null)
            {
                throw new ApplicationException("Expected object");
            }
        }
    }
}
