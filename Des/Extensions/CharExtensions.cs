using System;
using System.Collections.Generic;
using System.Text;

namespace Des.Extensions
{
    public static class CharExtensions
    {
        public static int HexToInt(this char hexChar)
        {
            hexChar = char.ToUpper(hexChar);

            return (int)hexChar < (int)'A' ?
                ((int)hexChar - (int)'0') :
                10 + ((int)hexChar - (int)'A');
        }
    }
}
