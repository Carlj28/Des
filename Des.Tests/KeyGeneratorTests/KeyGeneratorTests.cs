using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Des.Implementation;
using Xunit;

namespace Des.Tests.KeyGeneratorTests
{
    public class KeyGeneratorTests
    {
        public KeyGeneratorTests()
        {
        }

        [Fact]
        public void TestGeneratingKeys()
        {
            // Act
            var key = KeyGenerator.GetDESHexKey();

            // Assert
            Assert.True(key.Length == 16);
        }
    }
}
