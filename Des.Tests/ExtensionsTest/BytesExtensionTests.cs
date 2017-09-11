using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Des.Implementation;
using Xunit;

namespace Des.Tests.ExtensionsTest
{
    public class BytesExtensionTests
    {
        public BytesExtensionTests()
        {
        }

        [Fact]
        public void ByteArrayToStringTest()
        {
            var randomKey = KeyGenerator.GetDESHexKey();

            Assert.True(!String.IsNullOrEmpty(randomKey));
            Assert.True(randomKey.Length == 16);
        }
    }
}
