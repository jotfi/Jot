using System;
using System.Collections.Generic;
using System.Text;

namespace johncocom.Jot.Model.Base
{
    public interface ITransaction
    {
        string TableName();
        string CreateTable();
    }
}
