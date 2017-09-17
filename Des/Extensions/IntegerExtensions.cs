using System;
using System.Text;

namespace Des.Extensions
{
    /// <summary>
    /// Extensions for integer and integer array
    /// </summary>
    public static class IntegerExtensions
    {
        /// <summary>
        /// Converts int value to binary string
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns>Binary string</returns>
        public static string ConvertToBinary(this int value) => Convert.ToString(value, 2);

        /// <summary>
        /// Converts int value to binary string
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="stringLenth">Output string length</param>
        /// <returns>Binary string</returns>
        public static string ConvertToBinary(this int value, int stringLenth)
        {
            var binary = Convert.ToString(value, 2);

            if (binary.Length >= stringLenth) return binary;

            var builder = new StringBuilder(stringLenth);
            builder.Append('0', stringLenth - binary.Length);
            builder.Append(binary);

            return builder.ToString();
        }
    }
}
