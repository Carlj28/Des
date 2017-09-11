using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Des.Extensions;
using Des.Models;

namespace Des.Implementation
{
    internal static class DecodeWorker
    {
        public static string DecodeValue(string data, string hexKey)
        {
            var keys = SubkeysWorker.GenerateSubkeys(hexKey);

            var blocksOfData = DesCore.DivideValue(data);
            var processedData = new ConcurrentQueue<ValuePart>();
            var processExceptions = new ConcurrentQueue<Exception>();

            Parallel.ForEach(blocksOfData, (blockOfData) =>
            {
                try
                {
                    var blocks = DesCore.PrepareBlocks(blockOfData.Value.HexToBinary(), keys, false);

                    var reversedBlock = DesCore.ReverseLastBlock(blocks.Last());

                    processedData.Enqueue(new ValuePart(reversedBlock.BinaryStringToHexString(), blockOfData.Index));
                }
                catch (Exception e)
                {
                    processExceptions.Enqueue(e);
                }
            });

            //TODO: send details about ex
            if (processExceptions.Any())
                throw new Exception("One or more exceptions occurred!");

            var dataAsList = processedData.OrderBy(x => x.Index).ToList();
            var last = dataAsList.Last();
            var lastBlock = DesCore.RemoveAppendedFakeBits(last.Value);

            if (lastBlock.Length >= 16) return string.Join("", dataAsList.OrderBy(x => x.Index).Select(x => x.Value));

            dataAsList.Remove(last);
            dataAsList.Add(new ValuePart(lastBlock, last.Index));

            return string.Join("", dataAsList.Select(x => x.Value)).HexToString();
        }
    }
}
