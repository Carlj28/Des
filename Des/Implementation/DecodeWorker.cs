using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Des.Extensions;
using Des.Models;

namespace Des.Implementation
{
    public class DecodeWorker
    {
        public string DecodeValue(string data, string hexKey)
        {
            //var sb = new StringBuilder();

            var keys = SubkeysWorker.GenerateSubkeys(hexKey);

            Object lockMe = new Object();
            var blocksOfData = DesCore.DivideValue(data);
            var processedData = new List<ValuePart>();
            var exceptions = new ConcurrentQueue<Exception>();

            Parallel.ForEach(blocksOfData, (blockOfData) =>
            {
                try
                {
                    var blocks = DesCore.PrepareBlocks(blockOfData.Value.HexToBinary(), keys, true);

                    var reversedBlock = DesCore.ReverseLastBlock(blocks.Last());

                    lock (lockMe)
                    {
                        processedData.Add(new ValuePart(reversedBlock.BinaryStringToHexString(), blockOfData.Index));
                    }
                }
                catch (Exception e)
                {
                    exceptions.Enqueue(e);
                }
            });

            //foreach (var blockOfData in blocksOfData)
            //{
            //    var blocks = DesCore.PrepareBlocks(blockOfData.HexToBinary(), keys, false);

            //    var reversedBlock = DesCore.ReverseLastBlock(blocks.Last());

            //    sb.Append(reversedBlock.BinaryStringToHexString());
            //}

            var last = processedData.OrderBy(x => x.Index).Last();
            var lastBlock = DesCore.RemoveAppendedFakeBits(last.Value);

            if (lastBlock.Length >= 16) return string.Join("", processedData.OrderBy(x => x.Index).Select(x => x.Value));

            processedData.Remove(last);
            processedData.Add(new ValuePart(lastBlock, last.Index));

            return string.Join("", processedData.OrderBy(x => x.Index).Select(x => x.Value));
        }
    }
}
