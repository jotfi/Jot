﻿// Copyright 2020 John Cottrell
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

using jotfi.Jot.Base.System;
using jotfi.Jot.Core.Services.Base;
using jotfi.Jot.Model.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace jotfi.Jot.Core.Services.System
{
    public partial class OrganizationService : BaseService
    {
        public OrganizationService(Application app, LogOpts opts = null) : base(app, opts)
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
                if (AppSettings.IsClient)
                {
                    return CreateOrganizationClient(organization);
                }
                using var uow = Database.Context.Create();
                var conn = Database.Context.GetConnection();
                var organizationId = Repository.System.Organization.Insert(organization, conn);
                organizationId.IsEqualTo(1);
                uow.CommitAsync().Wait();
                return true;
            }
            catch (Exception ex)
            {
                Log(ex);
                return false;
            }            
        }
    }
}
