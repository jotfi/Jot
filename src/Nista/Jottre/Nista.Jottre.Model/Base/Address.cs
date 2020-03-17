using Nista.Jottre.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nista.Jottre.Model.Base
{
    public class Address : Transaction
    {
        public Entity Entity { get; set; }
        public string Lot { get; set; }
        public string Unit { get; set; }
        public string Number { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }

        public Address(Entity entity)
        {
            Entity = entity;
        }

        public override string CreateTable()
        {
            throw new NotImplementedException();
        }
    }
}
