using System;
using System.Collections.Generic;
using System.Text;

namespace KozzionCore.Tools
{
    public class ToolsString
    {
        public static string Repeat(string value_string, int count)
        {
            StringBuilder builder = new StringBuilder();
            for (int index = 0; index < count; index++)
            {
                builder.Append(value_string);
            }
            return builder.ToString();
        }

        public static string PadOrCrop(string value, int desired_length, char padding_char)
        {
            if (desired_length < value.Length)
            {
                return value.Substring(0, desired_length);
            }
            else
            {
                StringBuilder builder = new StringBuilder(value);
                for (int index = 0; index < desired_length - value.Length; index++)
                {
                    builder.Append(padding_char);
                }
                return builder.ToString();
            }

            
            
        }

        public static byte[] ConvertToBytes(string value_string)
        {
            byte[] bytes = new byte[value_string.Length * sizeof(char)];
            System.Buffer.BlockCopy(value_string.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string ConvertToString(byte[] array_bytes)
        {
            char[] chars = new char[array_bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(array_bytes, 0, chars, 0, array_bytes.Length);
            return new String(chars);
        }

        public static string ConvertToStringHex(string value_string)
        {    
            return string.Format("{0:X}", value_string);
        }

        public static string ConvertToStringHexadecimal(byte value_byte)
        {
            return BitConverter.ToString(new byte [] {value_byte});
        }

        public static string Reverse(string value_string)
        {
           if (value_string == null) 
           {
               return null;
           }
           char[] array = value_string.ToCharArray();
           Array.Reverse(array);
           return new string(array);
        }

        public static string DictionaryToString(Dictionary<string, string> dictionary)
        {
            string separator = "\t";
            foreach (string key in dictionary.Keys)
            {
                if (key.Contains(separator) || dictionary[key].Contains(separator))
                {
                    throw new Exception("Separator used");
                }
            }

            StringBuilder builder = new StringBuilder();
            foreach (string key in dictionary.Keys)
            {
                builder.Append(key);
                builder.Append(separator);
                builder.Append(dictionary[key]);
                builder.Append(separator);
            }
            return builder.ToString();
        }


        public static Dictionary<string, string> StringToDictionary(string value)
        {
            string separator = "\t";
            string [] string_array = value.Split(separator.ToCharArray()[0]);

            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            for (int index = 0; index < string_array.Length / 2; index++)
            {
                int index_key = index * 2;
                int index_value = (index * 2) + 1;
                dictionary[string_array[index_key]] = string_array[index_value];
            }
            return dictionary;
        }
    }
}
