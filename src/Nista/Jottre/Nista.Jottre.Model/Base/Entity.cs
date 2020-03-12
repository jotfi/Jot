using System;
using System.Collections.Generic;
using System.Text;

namespace Nista.Jottre.Model.Base
{
    public abstract class Entity : Transaction
    {
        public List<Entity> Entities { get; set; } = new List<Entity>();
    }
}
