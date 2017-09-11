using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Des.Implementation;
using Des.Extensions;
using Xunit;
using Xunit.Abstractions;

namespace Des.Tests.EncodeWorkerTests
{
    public class EncodeWorkerTests
    {
        private readonly EncodeWorker encodeWorker;
        private readonly DecodeWorker decodeWorker;
        private string hexKey = "133457799BBCDFF1";
        private readonly ITestOutputHelper testOutput;

        public EncodeWorkerTests(ITestOutputHelper testOutput)
        {
            // Arrange
            encodeWorker = new EncodeWorker();
            decodeWorker = new DecodeWorker();
            this.testOutput = testOutput;
        }

        [Theory]
        [InlineData("0123456789ABCDEF")]
        [InlineData("0123456789ABCDEF01")]
        public void EncodeTest(string value)
        {
            // Act
            var result = encodeWorker.EncodeValue(value, hexKey);
            var decoded = decodeWorker.DecodeValue(result, hexKey);

            // Assert
            Assert.True(value == decoded);
        }

        [Fact]
        public void OpenFileEncodeAndDecode()
        {
            DirectoryInfo d = new DirectoryInfo(@"Files/");//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*");
            string str = "";
            Stopwatch s = new Stopwatch();
            foreach (FileInfo file in Files)
            {
                s.Reset();
                s.Start();
                // Arrange
                var bytes = File.ReadAllBytes(file.DirectoryName + "\\" + file.Name);
                var filex = Convert.ToBase64String(bytes);
                var hexFile = filex.StringToHex();

                // Act
                var result = encodeWorker.EncodeValue(hexFile, hexKey);
                var decoded = decodeWorker.DecodeValue(result, hexKey);

                // Assert
                Assert.True((bool)(hexFile == decoded));
                s.Stop();
                testOutput.WriteLine($"Processed file {file.Name} {file.Length / 1000000} mb. in {s.Elapsed.Seconds} s. memory usage: {GC.GetTotalMemory(true)}");
            }

            //// Arrange
            //var bytes = File.ReadAllBytes("Files/TestFile1.txt");
            //var file = Convert.ToBase64String(bytes);
            //var hexFile = file.StringToHex();

            //// Act
            //var result = encodeWorker.EncodeValue(hexFile, hexKey);
            //var decoded = decodeWorker.DecodeValue(result, hexKey);

            //// Assert
            //Assert.True((bool) (hexFile == decoded));
        }
    }
}
