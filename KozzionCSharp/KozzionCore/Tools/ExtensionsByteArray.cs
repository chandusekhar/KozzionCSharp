namespace System
{
    public static class ExtensionsByteArray
    {
        public static string ToStringHexadecimal(this byte[] byte_array) 
        {
            return BitConverter.ToString(byte_array);
        }
    }
}
