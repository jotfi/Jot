using jotfi.Jot.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace jotfi.Jot.Model.System
{
    [Table("sqlite_master")]
    public class TableName : Transaction
    {
        [Column("name")]
        public string Name { get; set; }

        [Column("type")]
        public string Type { get; set; }

        public override string CreateTable()
        {
            throw new NotImplementedException();
        }
    }
}
