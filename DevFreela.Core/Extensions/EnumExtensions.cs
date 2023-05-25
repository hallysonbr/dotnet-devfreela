using System;
using System.ComponentModel;

namespace DevFreela.Core.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum en)
        {
            var fieldInfo = en.GetType().GetField(en.ToString());
            var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes != null && attributes.Length > 0
                ? attributes[0].Description
                : en.ToString();
        }

        public static string GetDescription<TEnum>(this object obj) where TEnum : struct
        {
            return Enum.TryParse(obj.ToString(), out TEnum en) 
                ? (en as Enum).GetDescription() 
                : obj.ToString();
        }
    }
}
