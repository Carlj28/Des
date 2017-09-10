using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Des.Implementation;
using Xunit;

namespace Des.Tests.EncodeWorkerTests
{
    public class EncodeWorkerTests
    {
        private readonly EncodeWorker encodeWorker;
        private readonly DecodeWorker decodeWorker;

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
            // Arrange
            var hexKey = "133457799BBCDFF1";

            // Act
            var result = encodeWorker.EncodeValue(value, hexKey);
            var decoded = decodeWorker.DecodeValue(result, hexKey);

            //Assert
            Assert.True(value == decoded);
        }
    }
}
