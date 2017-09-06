using System;
using System.Collections.Generic;
using System.Text;
using Des.Implementation;

namespace Des.Extensions
{
    public static class BytesExtensions
    {
        public static string ByteArrayToString(this byte[] ba)
        {
            var hex = BitConverter.ToString(ba);
            return hex.Replace("-", "");
        }
    }
}
