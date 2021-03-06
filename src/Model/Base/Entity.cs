﻿#region License
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

using System.Text.Json.Serialization;

namespace jotfi.Jot.Model.Base
{
    public abstract class Entity : Transaction, IEntity
    {
        public string? Code { get; set; }
        public string? Description { get; set; }
        [JsonIgnore]
        public string? CodePrefix { get; set; }
        public abstract string GetCodePrefix();
        public virtual void SetCodePrefix()
            => CodePrefix = GetCodePrefix();
        
        public virtual void SetCode(long seq)
            => Code = CodePrefix + seq;

    }
}
