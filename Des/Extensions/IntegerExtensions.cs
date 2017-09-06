using System;
using System.Text;

namespace Des.Extensions
{
    public static class IntegerExtensions
    {
        public static string ConvertToBinary(this int value) => Convert.ToString(value, 2);

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
