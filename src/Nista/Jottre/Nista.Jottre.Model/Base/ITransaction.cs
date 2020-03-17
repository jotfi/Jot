using System;
using System.Collections.Generic;
using System.Text;

namespace Nista.Jottre.Model.Base
{
    public interface ITransaction
    {
        string TableName();
        string CreateTable();
    }
}
