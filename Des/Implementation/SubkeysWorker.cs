using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Des.Extensions;
using Des.Models;
using Des.Validators;
using Des.Consts;

namespace Des.Implementation
{
    public class SubkeysWorker
    {
        public IEnumerable<Subkey> GenerateSubkeys(string hexKey)
        {
            Ensure.ValidateDesKey(hexKey);

            var binaryKey = hexKey.HexToBinary();

            var permutedKey = PermuteKey(binaryKey, Consts.Consts.PC1, 56);

            var c = permutedKey.Substring(0, 28);
            var d = permutedKey.Substring(28, 28);

            var blocks = PrepareBlocks(c, d);

            return blocks;
        }

        private string PermuteKey(string binaryKey, IReadOnlyList<int> PC, int keyLength)
        {
            var builder = new StringBuilder(keyLength);
            builder.Append('0', keyLength);

            for (var i = 0; i < PC.Count; i++)
            {
                builder[i] = binaryKey[PC[i] - 1];
            }

            return builder.ToString();
        }

        private IEnumerable<Subkey> PrepareBlocks(string c, string d)
        {
            var output = new List<Subkey> { new Subkey(c, d, string.Empty) };

            for (var i = 0; i < 16; i++)
            {
                var cn = String.Concat(output[i].C.Skip(Consts.Consts.SubkeyShiftSchedule[i]).Concat(output[i].C.Take(Consts.Consts.SubkeyShiftSchedule[i])));
                var dn = String.Concat(output[i].D.Skip(Consts.Consts.SubkeyShiftSchedule[i]).Concat(output[i].D.Take(Consts.Consts.SubkeyShiftSchedule[i])));

                output.Add(new Subkey(cn, dn, PermuteKey(cn + dn, Consts.Consts.PC2, 48)));
            }

            output.Remove(output.ElementAt(0));

            return output;
        }
    }
}
