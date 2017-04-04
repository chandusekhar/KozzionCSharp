using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionCore.Tools
{
    public class ToolsEnum
    {
        public static EnumType StringToEnum<EnumType>(string string_value)
        {
            return (EnumType)Enum.Parse(typeof(EnumType), string_value);
        }

        public static EnumType Int32ToEnum<EnumType>(int int_value)
        {
            return (EnumType)Enum.ToObject(typeof(EnumType), int_value);
        }

        public static string EnumToString<EnumType>(EnumType enum_value)
        {
            return Enum.GetName(typeof(EnumType), enum_value);
        }

        public static int EnumToInt32<EnumType>(EnumType enum_value)
        {
            Type genericType = typeof(EnumType);
            if (genericType.IsEnum)
            {
                return Convert.ToInt32(Enum.Parse(typeof(EnumType), enum_value.ToString()) as Enum);
            }
            else
            {
                throw new Exception("Type is not and enum type");
            }
        }
    }
}
