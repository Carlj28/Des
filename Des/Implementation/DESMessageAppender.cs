using System.Text;
using Des.Extensions;
using Des.Validators;

namespace Des.Implementation
{
    public static class DESMessageAppender
    {
        public static string AppendBits(string messageBlock)
        {
            Ensure.ArgumentNotNull(messageBlock, nameof(messageBlock));

            var messageInBits = messageBlock.HexToBinary();

            var appendTimes = messageBlock.Length % 16;

            var valueToAppend = appendTimes.ConvertToBinary(4);

            var sb = new StringBuilder(64);

            sb.Append(messageInBits);

            while (sb.Length < 64)
            {
                sb.Append(valueToAppend);
            }

            return sb.ToString().BinaryStringToHexString();
        }
    }
}
