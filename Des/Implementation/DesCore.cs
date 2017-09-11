using System.Collections.Generic;
using System.Linq;
using System.Text;
using Des.Extensions;
using Des.Models;

namespace Des.Implementation
{
    internal static class DesCore
    {
        static DesCore()
        {            
        }

        /// <summary>
        /// Reverse last block and apply IP-1
        /// </summary>
        /// <param name="block">Block</param>
        /// <returns>Reversed value</returns>
        public static string ReverseLastBlock(Block block)
        {
            var sb = new StringBuilder();
            sb.Append(block.R);
            sb.Append(block.L);

            return PermutationHelper.PermuteKey(sb.ToString(), Consts.Consts.reverseIP, 64);
        }

        public static IEnumerable<Block> PrepareBlocks(string blockOfData, IEnumerable<Subkey> keys, bool encode)
        {
            var permutedBlockOfData = PermutationHelper.PermuteKey(blockOfData, Consts.Consts.IP, 64);

            var l = permutedBlockOfData.Substring(0, 32);
            var r = permutedBlockOfData.Substring(32, 32);

            var data = new List<Block> { new Block(l, r) };

            if(encode)
                for (var i = 0; i < 16; i++)
                {
                    var ln = data[i].R;
                    var rl = data[i].L.XorByKey(F(keys.ElementAt(i).Key, data[i].R));

                    data.Add(new Block(ln, rl));
                }
            else
                for (var i = 0; i < 16; i++)
                {
                    var ln = data[i].R;
                    var rl = data[i].L.XorByKey(F(keys.ElementAt(15 - i).Key, data[i].R));

                    data.Add(new Block(ln, rl));
                }

            return data;
        }

        public static string F(string key, string r)
        {
            var er = PermutationHelper.PermuteKey(r, Consts.Consts.EBitSelection, 48);
            var xor = er.XorByKey(key);
            var sbox = PrepareSBoxes(xor);

            return sbox;
        }

        public static string PrepareSBoxes(string value)
        {
            //TODO ensure value has 48 bits

            var sb = new StringBuilder();

            for (var i = 0; i < 8; i++)
            {
                var group = value.Substring(6 * i, 6);
                var columnBits = group.Substring(0, 1) + group.Substring(5, 1);
                var rowBits = group.Substring(1, 4);

                var rowIndex = rowBits.BytesToInt();
                var columnIndex = columnBits.BytesToInt();

                var sval = Consts.Consts.STable[i][columnIndex][rowIndex];
                sb.Append(sval.ConvertToBinary(4));
            }

            return PermutationHelper.PermuteKey(sb.ToString(), Consts.Consts.P, 32);
        }

        public static IEnumerable<ValuePart> DivideValue(string valueInBits)
        {
            var blocksOfData = new List<ValuePart>();

            if (valueInBits.Length == 16)
            {
                blocksOfData.Add(new ValuePart(valueInBits, 0));
                return blocksOfData;
            }
            if (valueInBits.Length < 16)
                blocksOfData.Add(new ValuePart(valueInBits, 0));

            //Divide in block of 64 bits
            for (var i = 0; i < (double)valueInBits.Length / (double)16; i++)
            {
                blocksOfData.Add(new ValuePart(valueInBits.Length >= 16 * i + 16
                    ? valueInBits.Substring(16 * i, 16)
                    : valueInBits.Substring(16 * i, valueInBits.Length - 16 * i), i));
            }

            if (blocksOfData.Last().Value.Length == 16) return blocksOfData;

            var lastBlock = blocksOfData.Last();
            blocksOfData.RemoveAt(blocksOfData.Count - 1);
            blocksOfData.Add(new ValuePart(DESMessageAppender.AppendBits(lastBlock.Value), lastBlock.Index));

            return blocksOfData;
        }

        public static string RemoveAppendedFakeBits(string blockOfData)
        {
            var bitsCounter = blockOfData.Last().HexToInt();

            for (var i = 0; i < bitsCounter; i++)
            {
                if (blockOfData[15 - i].HexToInt() != bitsCounter)
                    return blockOfData;
            }

            return blockOfData.Substring(0, 16 - bitsCounter);
        }
    }
}
