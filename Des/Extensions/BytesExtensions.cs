using System;

namespace Des.Extensions
{
    /// <summary>
    /// Extensions for byte and byte array
    /// </summary>
    public static class BytesExtensions
    {
        /// <summary>
        /// Converts byte array to string
        /// </summary>
        /// <param name="ba">Byte array.</param>
        /// <returns>Converted value as string.</returns>
        public static string ByteArrayToString(this byte[] ba)
        {
            var hex = BitConverter.ToString(ba);
            return hex.Replace("-", "");
        }
    }
}
