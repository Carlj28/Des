using Des.Extensions;
using Xunit;

namespace Des.Tests.PermutationTests
{
    public class PermutationTests
    {
        [Fact]
        public void TestETble()
        {
            // Arrange
            var r = "11110000101010101111000010101010";
            var expectedPermutationResult = "011110100001010101010101011110100001010101010101";

            // Act
            var response = PermutationHelper.PermuteKey(r, Consts.Consts.EBitSelection, 48);

            // Assert
            Assert.Equal(expectedPermutationResult, response);
        }

        [Fact]
        public void ReversePermutaionTest()
        {
            // Arrange
            var r = "11110000101010101111000010101010";
            var expectedPermutationResult = "011110100001010101010101011110100001010101010101";

            // Act
            var permuted = PermutationHelper.PermuteKey(r, Consts.Consts.EBitSelection, 48);

            // Assert
            Assert.Equal(expectedPermutationResult, permuted);
        }
    }
}
