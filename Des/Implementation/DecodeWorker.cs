using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Des.Extensions;
using Des.Models;

namespace Des.Implementation
{
    public class DecodeWorker
    {
        public SubkeysWorker subkeysWorker { get; } = new SubkeysWorker();

        public string DecodeValue(string data, string hexKey)
        {
            var keys = subkeysWorker.GenerateSubkeys(hexKey);

            var blocksOfData = DivideValue(data);

            var output = string.Empty;

            //TODO: LINQ string builder
            foreach (var blockOfData in blocksOfData)
            {
                var blocks = EncodeBlock(blockOfData.StringToBinary(), keys);

                var reversedBlock = ReverseLastBlock(blocks.Last());

                output += Convert.ToInt64(reversedBlock, 2).ToString("X");
            }

            return output.HexToString();
        }

        //private string ReverseValue(string value) => PermutationHelper.ReversePermuteKey(value, Consts.Consts.reverseIP, 64);

        //TODO: string builder
        private string ReverseLastBlock(Block block) => PermutationHelper.PermuteKey(block.R + block.L, Consts.Consts.reverseIP, 64);

        private IEnumerable<Block> EncodeBlock(string blockOfData, IEnumerable<Subkey> keys)
        {
            var permutedBlockOfData = PermutationHelper.PermuteKey(blockOfData, Consts.Consts.IP, 64);

            var l = permutedBlockOfData.Substring(0, 32);
            var r = permutedBlockOfData.Substring(32, 32);

            var data = new List<Block> { new Block(l, r) };

            for (var i = 0; i < 16; i++)
            {
                var ln = data[i].R;
                var rl = data[i].L.XorByKey(F(keys.ElementAt(15 - i).Key, data[i].R));

                data.Add(new Block(ln, rl));
            }

            return data;
        }

        private string F(string key, string r)
        {
            var er = PermutationHelper.PermuteKey(r, Consts.Consts.EBitSelection, 48);
            var xor = er.XorByKey(key);
            var sbox = PrepareSBoxes(xor);

            return sbox;
        }

        private string PrepareSBoxes(string value)
        {
            //TODO ensure value has 48 bits

            //TODO: stringbuilder
            var output = string.Empty;

            for (var i = 0; i < 8; i++)
            {
                var group = value.Substring(6 * i, 6);
                var columnBits = group.Substring(0, 1) + group.Substring(5, 1);
                var rowBits = group.Substring(1, 4);

                var rowIndex = rowBits.BytesToInt();
                var columnIndex = columnBits.BytesToInt();

                //TODO: repolace 0
                var sval = Consts.Consts.STable[i][columnIndex][rowIndex];
                output += sval.ConvertToBinary(4);
            }

            return PermutationHelper.PermuteKey(output, Consts.Consts.P, 32);
        }

        private IEnumerable<string> DivideValue(string valueInBits)
        {
            //TODO: not working for string les than 64 bits
            if (valueInBits.Length <= 16)
                return new List<string> { valueInBits };

            //TODO: to linq
            var blocksOfData = new List<string>();

            //Divide in block of 64 chars
            for (var i = 0; i < valueInBits.Length / 16; i++)
            {
                blocksOfData.Add(valueInBits.Substring(15 * i, 16));
            }

            return blocksOfData;
        }
    }
}
