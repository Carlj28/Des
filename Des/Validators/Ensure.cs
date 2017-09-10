using System;
using System.Collections.Generic;
using System.Linq;
using Des.Extensions;
using Des.Models;

namespace Des.Validators
{
    public static class Ensure
    {
        /// <summary>
        /// Ensures that argument is not null
        /// </summary>
        /// <param name="argument">Argument</param>
        /// <param name="argumentName">Argument name</param>
        public static void ArgumentNotNull(object argument, string argumentName)
        {
            if(argument == null)
                throw new ArgumentException($"{argumentName} was null!");
        }

        /// <summary>
        /// Ensures that string value is not null or empty
        /// </summary>
        /// <param name="argument">Argument</param>
        /// <param name="argumentName">Argument name</param>
        public static void ArgumentNotNull(string argument, string argumentName)
        {
            if (String.IsNullOrEmpty(argument))
                throw new ArgumentException($"{argumentName} was null!");
        }

        /// <summary>
        /// Validates if key is proper DES key
        /// </summary>
        /// <param name="hexHey">Key for DES in hex</param>
        public static void ValidateDesKey(string hexHey)
        {
            if(hexHey.HexToBinary().Length != 64)
                throw new ArgumentException($"{nameof(hexHey)} is the wrong length!");
        }

        /// <summary>
        /// Validates DES subkeys
        /// </summary>
        /// <param name="subkeys">DES subkeys</param>
        public static void ValidateSubkeys(this IEnumerable<Subkey> subkeys)
        {
            if(subkeys.Any(x => x.C.Length != 28 || x.D.Length != 28))
                throw new ArgumentException($"{nameof(subkeys)} has the wrong length!");
        }
    }
}
