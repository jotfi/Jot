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
