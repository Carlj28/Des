using System.Security.Cryptography;
using Des.Extensions;

namespace Des.Implementation
{
    public class KeyGenerator
    {
        public string GetDESHexKey()
        {
            var bytes = GenerateRandomNumber(8);

            return bytes.ByteArrayToString();
        }

        private byte[] GenerateRandomNumber(int length)
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
