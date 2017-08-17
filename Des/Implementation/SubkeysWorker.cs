using System.Text;
using Des.Extensions;
using Des.Validators;

namespace Des.Implementation
{
    public class SubkeysWorker
    {
        public void GenerateSubkeys(string hexKey)
        {
            Ensure.ValidateDesKey(hexKey);

            var binaryKey = hexKey.HexToBinary();

            var permutedKey = PermuteKeyByPC1(binaryKey);
        }

        private string PermuteKeyByPC1(string binaryKey)
        {
            var builder = new StringBuilder("00000000000000000000000000000000000000000000000000000000", 57);

            for (var i = 0; i < Consts.Consts.PC1.Length; i++)
            {
                builder[i] = binaryKey[Consts.Consts.PC1[i] - 1];
            }

            return builder.ToString();
        }
    }
}
