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
        public void EncodeTest(string value)
        {
            // Act
            var result = encodeWorker.EncodeValue(value, "133457799BBCDFF1");
            var decoded = decodeWorker.DecodeValue(result, "133457799BBCDFF1");

            //Assert
            Assert.True(result == "85E813540F0AB405");
            Assert.True(value == decoded);
        }
    }
}
