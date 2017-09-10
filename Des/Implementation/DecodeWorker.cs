using System.Collections.Generic;
using System.Linq;
using System.Text;
using Des.Extensions;

namespace Des.Implementation
{
    public class DecodeWorker
    {
        public string DecodeValue(string data, string hexKey)
        {
            var sb = new StringBuilder();

            var keys = SubkeysWorker.GenerateSubkeys(hexKey);

            var blocksOfData = DesCore.DivideValue(data);

            //TODO: LINQ string builder
            foreach (var blockOfData in blocksOfData)
            {
                var blocks = DesCore.PrepareBlocks(blockOfData.HexToBinary(), keys, false);

                var reversedBlock = DesCore.ReverseLastBlock(blocks.Last());

                sb.Append(reversedBlock.BinaryStringToHexString());
            }

            var lastBlock = DesCore.RemoveAppendedFakeBits(sb.ToString(sb.Length - 16, 16));

            if (lastBlock.Length < 16)
            {
                sb.Remove(sb.Length - 16, 16);
                sb.Append(lastBlock);
            }

            return sb.ToString();
        }
    }
}
