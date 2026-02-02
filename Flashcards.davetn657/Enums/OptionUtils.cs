using System.ComponentModel;
using System.Reflection;

namespace Flashcards.davetn657.Enums;
public class OptionUtils
{
    public static string GetStringValue(Enum value)
    {
        FieldInfo? info = value.GetType().GetField(value.ToString());
        DescriptionAttribute[] attributes = (DescriptionAttribute[])info.GetCustomAttributes(typeof(DescriptionAttribute), false);
        if (attributes.Length > 0)
        {
            return attributes[0].Description;
        }
        else
        {
            return string.Empty;
        }
    }

    public static Enum GetEnumValue(string description, Type enumType)
    {
        var enumValues = enumType.GetEnumValues();

        foreach (var value in enumValues)
        {
            if(GetStringValue((Enum)value).Equals(description))
            {
                return (Enum)value;
            }
        }
        throw new Exception("Not Found.");
    }
}
