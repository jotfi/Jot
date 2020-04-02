using jotfi.Jot.Database.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace jotfi.Jot.Core.Services.Base
{
    public interface IService
    {
        DbContext GetContext();
    }
}
