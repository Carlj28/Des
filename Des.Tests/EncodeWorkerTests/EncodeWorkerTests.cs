using System;
using System.Diagnostics;
using System.IO;
using Des.Extensions;
using Xunit;
using Xunit.Abstractions;

namespace Des.Tests.EncodeWorkerTests
{
    public class EncodeWorkerTests
    {
        private string hexKey = "133457799BBCDFF1";
        private readonly ITestOutputHelper testOutput;

        public EncodeWorkerTests(ITestOutputHelper testOutput)
        {
            // Arrange
            this.testOutput = testOutput;
        }

        [Theory]
        [InlineData("0123456789ABCDEF")]
        [InlineData("0123456789ABCDEF01")]
        public void EncodeTest(string value)
        {
            // Act
            var result = Des.Encode(value, hexKey);
            var decoded = Des.Decode(result, hexKey);

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
            Assert.True((bool)(hexFile == decoded));
        }
    }
}
