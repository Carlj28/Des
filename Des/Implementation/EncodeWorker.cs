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
    public class EncodeWorker
    {
        public string EncodeValue(string data, string hexKey)
        {
            //var sb = new StringBuilder();

            var keys = SubkeysWorker.GenerateSubkeys(hexKey);

            Object lockMe = new Object();
            var blocksOfData = DesCore.DivideValue(data);
            var processedData = new List<ValuePart>();
            // Use ConcurrentQueue to enable safe enqueueing from multiple threads.
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
            //    var blocks = DesCore.PrepareBlocks(blockOfData.HexToBinary(), keys, true);

            //    var reversedBlock = DesCore.ReverseLastBlock(blocks.Last());

            //    sb.Append(reversedBlock.BinaryStringToHexString());
            //}

            return string.Join("", processedData.OrderBy(x => x.Index).Select(x => x.Value));
        }  
    }
}
