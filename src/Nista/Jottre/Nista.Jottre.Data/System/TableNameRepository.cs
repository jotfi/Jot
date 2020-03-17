using Nista.Jottre.Data.Base;
using Nista.Jottre.Database.Base;
using Nista.Jottre.Model.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nista.Jottre.Data.System
{
    public class TableNameRepository : BaseRepository<TableName>
    {
        public TableNameRepository(IDbContext context) : base(context)
        {

        }

        public void GetList()
        {
            using (Context.Create())
            {
                var list = Context.GetConnection().GetList<TableName>(new { Type = "table" });
            }
        }
    }
}
