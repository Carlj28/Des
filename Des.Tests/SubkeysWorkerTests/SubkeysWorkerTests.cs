using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Des.Extensions;
using Des.Implementation;
using Xunit;

namespace Des.Tests.SubkeysWorkerTests
{
    public class SubkeysWorkerTests
    {
        private readonly SubkeysWorker subkeysWorker;

        public SubkeysWorkerTests()
        {
            // Arrange
            subkeysWorker = new SubkeysWorker();
        }

        [Fact]
        public void HexToBinaryTest()
        {
            // Act
            var response = "133457799BBCDFF1".HexToBinary();

            // Assert
            Assert.Equal("0001001100110100010101110111100110011011101111001101111111110001", response);
        }
    }
}
