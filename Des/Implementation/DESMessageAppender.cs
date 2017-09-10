using System.Text;
using Des.Extensions;
using Des.Validators;

namespace Des.Implementation
{
    public static class DESMessageAppender
    {
        /// <summary>
        /// Appends bits according to RFC 2315, section 10.3, Note 2
        /// https://tools.ietf.org/html/rfc2315#page-22
        /// </summary>
        /// <param name="messageBlock"></param>
        /// <returns></returns>
        public static string AppendBits(string messageBlock)
        {
            Ensure.ArgumentNotNull(messageBlock, nameof(messageBlock));

            var messageInBits = messageBlock.HexToBinary();

            var appendTimes = 16 - messageBlock.Length % 16;

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
