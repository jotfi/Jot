using System;
using System.Collections.Generic;
using System.Text;

namespace Nista.Jottre.Model.Base
{
    public abstract class Entity : Transaction
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public List<Entity> Entities { get; set; } = new List<Entity>();

        public static string EntityFields() =>
            @"
    Code varchar(100) not null,
    Description text not null";
    }
}
