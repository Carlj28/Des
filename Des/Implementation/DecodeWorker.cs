using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Des.Extensions;

namespace Des.Implementation
{
    public class DecodeWorker
    {
        public SubkeysWorker subkeysWorker { get; } = new SubkeysWorker();

        public void DecodeValue(string data, string hexKey)
        {
            var keys = subkeysWorker.GenerateSubkeys(hexKey);

            var blocksOfData = DivideValue(data);

            var output = string.Empty;

            //TODO: LINQ string builder
            foreach (var blockOfData in blocksOfData)
            {
                //var blocks = EncodeBlock(blockOfData.StringToBinary(), keys);

                //var reversedBlock = ReverseLastBlock(blocks.Last());

                //output += Convert.ToInt64(reversedBlock, 2).ToString("X");
            }
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
