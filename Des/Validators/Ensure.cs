using System;
using Des.Extensions;

namespace Des.Validators
{
    public static class Ensure
    {
        public static void ValidateDesKey(string hexHey)
        {
            if(hexHey.HexToBinary().Length != 64)
                throw new ArgumentException($"{nameof(hexHey)} is the wrong length!");
        }
    }
}
