using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace jotfi.Jot.Base.Utils
{
    public static class Extensions
    {
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
