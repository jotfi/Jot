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

using jotfi.Jot.Base.Settings;
using jotfi.Jot.Base.System;
using jotfi.Jot.Core.Services.Base;
using jotfi.Jot.Database.Classes;
using jotfi.Jot.Database.Repository.System;
using jotfi.Jot.Model.System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace jotfi.Jot.Core.Services.System
{
    public partial class OrganizationService : BaseService<OrganizationService, OrganizationRepository>
    {

        public OrganizationService(IServiceProvider services) : base(services)
        {
        }

        public bool SaveOrganization(Organization organization, out string error)
        {
            if (!IsOrganizationValid(organization, out error))
            {
                return false;
            }
            return CreateOrganization(organization);
        }
        public bool IsOrganizationValid(Organization organization, out string error)
        {
            error = string.Empty;
            if (string.IsNullOrWhiteSpace(organization.Name))
            {
                error += "Invalid organization. Name must not be blank.";
                return false;
            }
            return true;
        }

        public bool CreateOrganization(Organization organization)
        {
            try
            {
                if (Settings.IsClient)
                {
                    return CreateOrganizationClient(organization);
                }
                var organizationId = Repository.Insert(organization);
                organizationId.IsNotZero();
                return true;
            }
            catch (Exception ex)
            {
                Log.LogError(ex, ex.Message);
                return false;
            }            
        }
    }
}
