using Dapper.FluentMap.Dommel.Mapping;
using jotfi.Jot.Model.Base;
using jotfi.Jot.Model.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace jotfi.Jot.Database.Primitives
{
    public class AddressMap : DommelEntityMap<Address>
    {
        public AddressMap()
        {
            Map(p => p.Id).IsKey();
        }
    }
}
