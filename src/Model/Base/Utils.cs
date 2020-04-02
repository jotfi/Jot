using System;
using System.Collections.Generic;
using System.Text;

namespace jotfi.Jot.Model.Base
{
    public class Utils
    {
        public static string GetCodePrefix(string? field1, string? field2 = "")
        {
            var hasBoth = !string.IsNullOrEmpty(field1) && !string.IsNullOrEmpty(field2);
            var fieldLength = hasBoth ? 3 : 6;
            string codePrefix = string.Empty;
            if (!string.IsNullOrEmpty(field1))
            {
                codePrefix += field1.PadRight(fieldLength).Substring(0, fieldLength).Trim();
            }
            if (!string.IsNullOrEmpty(field2))
            {
                codePrefix += field2.PadRight(fieldLength).Substring(0, fieldLength).Trim();
            }
            if (string.IsNullOrEmpty(codePrefix))
            {
                codePrefix = "zblank";
            }
            return codePrefix.ToUpper();
        }
    }
}
