using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Des.Extensions
{
    public static class PermutationHelper
    {
        /// <summary>
        /// Permute key by table
        /// </summary>
        /// <param name="value">Key</param>
        /// <param name="PC">Permutation table</param>
        /// <param name="keyLength">Output length</param>
        /// <returns>Permuted value</returns>
        public static string PermuteKey(string value, IReadOnlyList<int> PC, int keyLength)
        {
            var builder = new StringBuilder(keyLength);
            builder.Append('0', keyLength);

            //for (var i = 0; i < PC.Count; i++)
            //{
            //    builder[i] = value[PC[i] - 1];
            //}

            Parallel.For(0, PC.Count,
             index => {
                 builder[index] = value[PC[index] - 1];
             });

            return builder.ToString();
        }
    }
}
