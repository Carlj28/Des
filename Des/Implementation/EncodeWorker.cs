using System.Linq;
using System.Text;
using Des.Extensions;

namespace Des.Implementation
{
    public class EncodeWorker
    {
        public string EncodeValue(string data, string hexKey)
        {
            var sb = new StringBuilder();

            var keys = SubkeysWorker.GenerateSubkeys(hexKey);

            var blocksOfData = DesCore.DivideValue(data);

            foreach (var blockOfData in blocksOfData)
            {
                var blocks = DesCore.PrepareBlocks(blockOfData.HexToBinary(), keys, true);

                var reversedBlock = DesCore.ReverseLastBlock(blocks.Last());

                sb.Append(reversedBlock.BinaryStringToHexString());
            }

            return sb.ToString();
        }  
    }
}
