using System;
using System.Collections.Generic;
using System.Text;

namespace Des.Models
{
    public class EncodedValueAndKey
    {
        public EncodedValueAndKey(string encodedValue, string key)
        {
            EncodedValue = encodedValue;
            Key = key;
        }

        public string EncodedValue { get; set; }
        public string Key { get; set; }
    }
}
