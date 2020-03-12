using Nista.Jottre.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nista.Jottre.Model.Base
{
    public class Email : Transaction
    {
        public Entity Entity { get; set; }
        public string EmailAddress { get; set; }

        public Email(Entity entity, string emailAddress) => (Entity, EmailAddress) = (entity, emailAddress);
        
    }
}
