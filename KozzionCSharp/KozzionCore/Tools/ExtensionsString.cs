using KozzionCore.Tools;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace System
{
    public static class ExtensionsString
    {

        public static bool ContainsAny(string source, string[] to_test)
        {
            foreach (string test in to_test)
            {
                if (source.Contains(test))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool ContainsAny<ArrayType>(this string source, string [] to_test)
        {
            foreach (string test in to_test)
            {
                if (source.Contains(test))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
