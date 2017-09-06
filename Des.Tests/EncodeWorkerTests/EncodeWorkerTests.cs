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

        public EncodeWorkerTests()
        {
            // Arrange
            encodeWorker = new EncodeWorker();
        }

        [Theory]
        [InlineData("ala ma kota i dwa psy")]
        [InlineData("ala ma kota i dwa psy asd ")]
        [InlineData("ala ma kota i dwa psy fsd asd ")]
        [InlineData("ala ma kota i dwa psy, ala ma kota i dwa psy")]
        [InlineData("ala ma kota i dwa psy, ala ma kota i dwa psy, ala ma kota i dwa psy")]
        public void EncodeTest(string value)
        {
            // Act
            var result = encodeWorker.EncodeValue(value, "133457799BBCDFF1");

            //Assert
            Assert.True(!String.IsNullOrEmpty(result));
        }
    }
}
