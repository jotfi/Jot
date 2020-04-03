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

using Dommel.Json;
using jotfi.Jot.Model.Base;

namespace jotfi.Jot.Model.System
{
    public class Person : Entity 
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [JsonData]
        public ContactData? Contact { get; set; }
        [JsonData]
        public Address? Address { get; set; }

        public override string GetCodePrefix()
            => Utils.GetCodePrefix(LastName, FirstName);
    }
}
