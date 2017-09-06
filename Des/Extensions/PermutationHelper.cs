using System;
using System.Collections.Generic;
using System.Text;

namespace Des.Extensions
{
    public static class PermutationHelper
    {
        public static string PermuteKey(string value, IReadOnlyList<int> PC, int keyLength)
        {
            var builder = new StringBuilder(keyLength);
            builder.Append('0', keyLength);

            for (var i = 0; i < PC.Count; i++)
            {
                builder[i] = value[PC[i] - 1];
            }

            return builder.ToString();
        }

        public static string ReversePermuteKey(string value, IReadOnlyList<int> PC, int keyLength)
        {
            var builder = new StringBuilder(keyLength);
            builder.Append('0', keyLength);

            for (var i = 0; i < value.Length; i++)
            {
                builder[PC[i] - 1] = value[i];
            }

            return builder.ToString();
        }
    }
}
