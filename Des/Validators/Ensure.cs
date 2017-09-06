﻿using System;
using System.Collections.Generic;
using System.Linq;
using Des.Extensions;
using Des.Models;

namespace Des.Validators
{
    public static class Ensure
    {
        public static void ArgumentNotNull(object argument, string argumentName)
        {
            if(argument == null)
                throw new ArgumentException($"{argumentName} was null!");
        }

        public static void ArgumentNotNull(string argument, string argumentName)
        {
            if (String.IsNullOrEmpty(argument))
                throw new ArgumentException($"{argumentName} was null!");
        }

        public static void ValidateDesKey(string hexHey)
        {
            if(hexHey.HexToBinary().Length != 64)
                throw new ArgumentException($"{nameof(hexHey)} is the wrong length!");
        }

        public static void ValidateSubkeys(this IEnumerable<Subkey> subkeys)
        {
            if(subkeys.Any(x => x.C.Length != 28 || x.D.Length != 28))
                throw new ArgumentException($"{nameof(subkeys)} has the wrong length!");
        }
    }
}
