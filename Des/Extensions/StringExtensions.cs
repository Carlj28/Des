using System;
using System.Linq;
using System.Text;

namespace Des.Extensions
{
    /// <summary>
    /// Extensions for string and string array
    /// </summary>
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
            var sb = new StringBuilder();
            foreach (var b in Encoding.Unicode.GetBytes(value))
            {
                sb.Append(Convert.ToString(b, 2));
            }
            return sb.ToString();
        }

        /// <summary>
        /// Converts binary string to hex string
        /// </summary>
        /// <param name="binary">Binary string</param>
        /// <returns>Hex string</returns>
        public static string BinaryStringToHexString(this string binary)
        {
            var result = new StringBuilder(binary.Length / 4 + 1);

            var mod4Len = binary.Length % 4;
            if (mod4Len != 0)
            {
                binary = binary.PadLeft(((binary.Length / 4) + 1) * 4, '0');
            }

            for (var i = 0; i < binary.Length; i += 4)
            {
                var fourBits = binary.Substring(i, 4);
                result.AppendFormat("{0:X}", Convert.ToByte(fourBits, 2));
            }

            return result.ToString();
        }

        /// <summary>
        /// Converts string to hex
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns>Hex string</returns>
        public static string StringToHex(this string value)
        {
            var bytes = Encoding.Default.GetBytes(value);
            return BitConverter.ToString(bytes).Replace("-", "");
        }

        /// <summary>
        /// Converts hex string to string
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns>Hex string</returns>
        public static string HexToString(this string value)
        {
            var raw = new byte[value.Length / 2];
            for (var i = 0; i < raw.Length; i++)
            {
                raw[i] = Convert.ToByte(value.Substring(i * 2, 2), 16);
            }
            return Encoding.UTF8.GetString(raw);
        }

        /// <summary>
        /// Xor string by key
        /// </summary>
        /// <param name="value"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string XorByKey(this string value, string key)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < value.Length; i++)
                sb.Append(value[i] ^ key[i % key.Length]);

            return sb.ToString();
        }

        /// <summary>
        /// Converts bytes string to integer value
        /// </summary>
        /// <param name="value">Bytes string</param>
        /// <returns>Integer value</returns>
        public static int BytesToInt(this string value) => Convert.ToInt32(value, 2);
    }
}
