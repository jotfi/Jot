using jotfi.Jot.Base.Utils;
using jotfi.Jot.Model.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace jotfi.Jot.Core.Services.System
{
    public partial class OrganizationService
    {
        public bool CreateOrganizationClient(Organization organization)
        {
            try
            {
                var response = App.Client.PostAsync("organization", organization.ToContent()).Result;
                response.EnsureSuccessStatusCode();
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
