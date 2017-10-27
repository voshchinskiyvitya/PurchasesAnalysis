using System;
using System.ComponentModel;

namespace PurchasesAnalysis.Core.Extentions
{
    public static class EnumExtentions
    {
        public static string GetDescription<T>(this T value) where T: struct
        {
            var type = typeof(T);
            if (!type.IsEnum)
                throw new InvalidOperationException("The argument is not Enum.");

            foreach (var field in type.GetFields())
            {
                var attr = Attribute.GetCustomAttribute(field, typeof (DescriptionAttribute)) as DescriptionAttribute;
                if (attr != null)
                {
                    var val = field.GetValue(value);
                    if (val.Equals(value))
                    {
                        return attr.Description;
                    }
                }
            }

            throw new ArgumentException("Not found.", nameof(value));
        }

        public static T GetValueByDescription<T>(this string description) where T : struct
        {
            var type = typeof(T);
            if (!type.IsEnum)
                throw new InvalidOperationException("The argument is not Enum.");

            var enumUnderlyingType = Enum.GetUnderlyingType(type);
            var enumValues = System.Enum.GetValues(type);

            for (var i = 0; i < enumValues.Length; i++)
            {
                var value = (T)enumValues.GetValue(i);
                if (value.GetDescription() == description)
                {
                    var val = Convert.ChangeType(value, enumUnderlyingType);
                    return (T)Enum.Parse(type, val.ToString());
                }
            }

            return default(T);
            //throw new ArgumentException("Not found.", nameof(description));
        }
    }
}
