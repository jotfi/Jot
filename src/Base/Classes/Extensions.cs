using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace jotfi.Jot.Base.Classes
{
    public static class Extensions
    {
        public static string ToJson(this object obj) => JsonConvert.SerializeObject(obj);
        public static StringContent ToContent(this object obj) => new StringContent(obj.ToJson(), Encoding.UTF8, "application/json");

        public static bool ToBool(this object source, bool def = false)
        {
            if (source == null)
            {
                return def;
            }
            if (source is bool)
            {
                return (bool)source;
            }
            if (bool.TryParse(source.ToString(), out bool result))
            {
                return result;
            }
            return def;
        }

        public static int ToInt(this object source, int def = 0)
        {
            if (source == null)
            {
                return def;
            }
            if (source is int)
            {
                return (int)source;
            }
            if (int.TryParse(source.ToString(), out int result))
            {
                return result;
            }
            return def;
        }

        public static decimal ToDec(this object source, int def = 0)
        {
            if (source == null)
            {
                return def;
            }
            if (source is decimal)
            {
                return (decimal)source;
            }
            if (source is int)
            {
                return (int)source;
            }
            if (decimal.TryParse(source.ToString(), out decimal result))
            {
                return result;
            }
            return def;
        }

        public static DateTime ToDateTime(this object source)
        {
            if (source == null)
            {
                return DateTime.MinValue;
            }
            if (source is DateTime)
            {
                return (DateTime)source;
            }
            if (DateTime.TryParse(source.ToString(), out DateTime result))
            {
                return result;
            }
            return DateTime.MinValue;
        }
    }
}
