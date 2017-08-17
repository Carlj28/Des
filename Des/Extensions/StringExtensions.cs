using System;
using System.Linq;

namespace Des.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Converts hex string to binary string
        /// </summary>
        /// <param name="hexValue">Hex string</param>
        /// <returns>Binary string</returns>
        public static string HexToBinary(this string hexValue) => String.Join(String.Empty,
          hexValue.Select(
            c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
    }
}
