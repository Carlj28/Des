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
        public KeyGenerator Generator { get; }

        public BytesExtensionTests()
        {
            Generator = new KeyGenerator();
        }

        [Fact]
        public void ByteArrayToStringTest()
        {
            var randomKey = Generator.GetDESHexKey();

            Assert.True(!String.IsNullOrEmpty(randomKey));
            Assert.True(randomKey.Length == 16);
        }
    }
}
