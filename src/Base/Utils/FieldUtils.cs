using jotfi.Jot.Base.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace jotfi.Jot.Base.Utils
{
    public class FieldUtils
    {
        public static void SetValue(object model, string id, string value)
        {
            var property = model.GetType().GetProperty(id);
            switch (property?.PropertyType.FullName.ToLower())
            {
                case "system.boolean":
                    property.SetValue(model, value.ToBool());
                    break;
                case "system.decimal":
                    property.SetValue(model, value.ToDec());
                    break;
                case "system.int32":
                    property.SetValue(model, value.ToInt());
                    break;
                case "system.datetime":
                    property.SetValue(model, value.ToDateTime());
                    break;
                default:
                    property.SetValue(model, value);
                    break;
            }
        }
    }
}
