using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Des.Extensions;
using Des.Models;

namespace Des.Implementation
{
    internal static class EncodeWorker
    {
        public static string EncodeValue(string data, string hexKey)
        {
            var keys = SubkeysWorker.GenerateSubkeys(hexKey);

            var blocksOfData = DesCore.DivideValue(data);
            var processedData = new ConcurrentQueue<ValuePart>();
            var processExceptions = new ConcurrentQueue<Exception>();

            Parallel.ForEach(blocksOfData, (blockOfData) =>
            {
                try
                {
                    var blocks = DesCore.PrepareBlocks(blockOfData.Value.HexToBinary(), keys, true);

                    var reversedBlock = DesCore.ReverseLastBlock(blocks.Last());

                    processedData.Enqueue(new ValuePart(reversedBlock.BinaryStringToHexString(), blockOfData.Index));
                }
                catch (Exception e)
                {
                    processExceptions.Enqueue(e);
                }
            });

            //TODO: send details about ex
            if(processExceptions.Any())
                throw new Exception("One or more exceptions occurred!");

            return string.Join("", processedData.OrderBy(x => x.Index).Select(x => x.Value));
        }  
    }
}
