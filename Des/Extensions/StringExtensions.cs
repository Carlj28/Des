using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        /// <summary>
        /// Converts string to binary
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string StringToBinary(this string value)
        {
            //TODO: shorten
            var sb = new StringBuilder();
            foreach (var b in Encoding.Unicode.GetBytes(value))
            {
                sb.Append(Convert.ToString(b, 2));
            }
            return sb.ToString();
        }

        public static string BinaryStringToHexString(this string binary)
        {
            var result = new StringBuilder(binary.Length / 8 + 1);

            // TODO: check all 1's or 0's... Will throw otherwise

            int mod4Len = binary.Length % 8;
            if (mod4Len != 0)
            {
                // pad to length multiple of 8
                binary = binary.PadLeft(((binary.Length / 8) + 1) * 8, '0');
            }

            for (int i = 0; i < binary.Length; i += 8)
            {
                string eightBits = binary.Substring(i, 8);
                result.AppendFormat("{0:X2}", Convert.ToByte(eightBits, 2));
            }

            return result.ToString();
        }

        /// <summary>
        /// Converts binary to string
        /// </summary>
        /// <param name="binary"></param>
        /// <returns></returns>
        public static string BinaryToString(this string binary)
        {
            //TODO: shorten
            var list = new List<byte>();

            for (var i = 0; i < binary.Length; i += 8)
            {
                String t = binary.Substring(i, 8);

                list.Add(Convert.ToByte(t, 2));
            }

            return Encoding.ASCII.GetString(list.ToArray());
        }

        /// <summary>
        /// Xor string by key
        /// </summary>
        /// <param name="value"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string XorByKey(this string value, string key)
        {
            //TODO: shorten
            var sb = new StringBuilder();
            for (var i = 0; i < value.Length; i++)
                sb.Append(value[i] ^ key[i % key.Length]);

            return sb.ToString();
        }

        public static int BytesToInt(this string value) => Convert.ToInt32(value, 2);
    }
}
