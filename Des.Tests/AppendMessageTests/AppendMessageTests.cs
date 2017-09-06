using Des.Implementation;
using Xunit;

namespace Des.Tests.AppendMessageTests
{
    public class AppendMessageTests
    {
        [Theory]
        [InlineData("A")]
        [InlineData("AA")]
        [InlineData("AAA")]
        [InlineData("AAAA")]
        [InlineData("AAAAA")]
        [InlineData("AAAAAA")]
        [InlineData("AAAAAAA")]
        [InlineData("AAAAAAAA")]
        [InlineData("AAAAAAAAA")]
        [InlineData("AAAAAAAAAA")]
        [InlineData("AAAAAAAAAAA")]
        [InlineData("AAAAAAAAAAAA")]
        [InlineData("AAAAAAAAAAAAA")]
        [InlineData("AAAAAAAAAAAAAA")]
        [InlineData("AAAAAAAAAAAAAAA")]
        public void TestAppendingMessage(string message)
        {
            // Act
            var result = DESMessageAppender.AppendBits(message);

            // Assert
            Assert.True(result.Length == 16);
        }
    }
}
