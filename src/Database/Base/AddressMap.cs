using Dapper.FluentMap.Dommel.Mapping;
using jotfi.Jot.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace jotfi.Jot.Database.Base
{
    public class AddressMap : DommelEntityMap<Address>
    {
        public AddressMap()
        {
            Map(p => p.Id).IsKey();
        }
    }
}
