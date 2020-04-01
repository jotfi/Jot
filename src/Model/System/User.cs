// Copyright 2020 John Cottrell
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

using Dommel;
using jotfi.Jot.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace jotfi.Jot.Model.System
{
    public class User : Transaction
    {
        public string? UserName { get; set; }
        [JsonIgnore]
        public byte[]? PasswordHash { get; set; }
        [JsonIgnore]
        public byte[]? PasswordSalt { get; set; }
        public long PersonId { get; set; }
        public Person Person { get; set; } = new Person();

        [Ignore, NotMapped]
        public string? Token { get; set; }

        [Ignore, NotMapped, Display(Name = "User Password")]
        public string? CreatePassword { get; set; }

        [Ignore, NotMapped, Display(Name = "Confirm Password")]
        public string? ConfirmPassword { get; set; }
    }
}
