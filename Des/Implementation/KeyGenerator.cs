using System.Security.Cryptography;
using Des.Extensions;

namespace Des.Implementation
{
    public static class KeyGenerator
    {
        /// <summary>
        /// Generates random DES key
        /// </summary>
        /// <returns></returns>
        public static string GetDESHexKey()
        {
            var bytes = GenerateRandomNumber(8);

            return bytes.ByteArrayToString();
        }

        private static byte[] GenerateRandomNumber(int length)
        {
            using (var randomNumberGenerator = RandomNumberGenerator.Create())
            {
                var number = new byte[length];
                randomNumberGenerator.GetBytes(number);

                return number;
            }
        }
    }
}
