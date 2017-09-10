using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Des.Implementation;
using Des.Extensions;
using Xunit;

namespace Des.Tests.EncodeWorkerTests
{
    public class EncodeWorkerTests
    {
        private readonly EncodeWorker encodeWorker;
        private readonly DecodeWorker decodeWorker;
        private string hexKey = "133457799BBCDFF1";

        public EncodeWorkerTests()
        {
            // Arrange
            encodeWorker = new EncodeWorker();
            decodeWorker = new DecodeWorker();
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
            // Arrange
            var bytes = File.ReadAllBytes("Files/TestFile1.txt");
            var file = Convert.ToBase64String(bytes);
            var hexFile = file.StringToHex();

            // Act
            var result = encodeWorker.EncodeValue(hexFile, hexKey);
            var decoded = decodeWorker.DecodeValue(result, hexKey);

            // Assert
            Assert.True((bool) (hexFile == decoded));
        }
    }
}
