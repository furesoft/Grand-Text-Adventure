using System;
using System.ComponentModel;
using System.Globalization;

namespace GrandTextAdventure.Core.Parser
{
    [TypeConverter(typeof(StringTokenConverter))]
    public class StringTokenConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return value.ToString()[1..^1];
        }
    }
}
