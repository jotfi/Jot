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

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jotfi.Jot.Model.Base
{
    public class ContactData
    {
        [Display(Name = "Email Address")]
        public string? EmailAddress { get; set; }
        
        [NotMapped]
        [Display(Name = "Confirm Email")]
        public string? ConfirmEmail { get; set; }

        [Display(Name = "Mobile Phone")]
        public string? MobilePhone { get; set; }

        [Display(Name = "Home Phone")]
        public string? HomePhone { get; set; }

        [Display(Name = "Work Phone")]
        public string? WorkPhone { get; set; }
    }
}
