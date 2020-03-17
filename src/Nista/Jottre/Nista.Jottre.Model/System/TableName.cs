using Nista.Jottre.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Nista.Jottre.Model.System
{
    [Table("sqlite_master")]
    public class TableName : Transaction
    {
        [Column("name")]
        public string Name { get; set; }

        [Column("type")]
        public string Type { get; set; }
    }
}
