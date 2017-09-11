using Des.Extensions;
using Des.Implementation;
using Des.Validators;

namespace Des
{
    public static class Des
    {
        /// <summary>
        /// Encodes data for key.
        /// </summary>
        /// <param name="data">Data.</param>
        /// <param name="dataInHex">Data is in hex format?</param>
        /// <param name="hexKey">Key.</param>
        /// <returns>Encoded data.</returns>
        public static string Encode(string data, string hexKey)
        {
            Ensure.ArgumentNotNull(data, nameof(data));
            Ensure.ValidateDesKey(hexKey);

            return EncodeWorker.EncodeValue(data.StringToHex(), hexKey);
        }

        /// <summary>
        /// Encodes data and generates random key.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="dataInHex"></param>
        /// <returns>Encoded data.</returns>
        public static (string encodedValue, string key) Encode(string data, bool dataInHex)
        {
            Ensure.ArgumentNotNull(data, nameof(data));

            var key = KeyGenerator.GetDESHexKey();

            return (EncodeWorker.EncodeValue(dataInHex ? data : data.StringToHex(), key), key);
        }

        /// <summary>
        /// Decodes data for key.
        /// </summary>
        /// <param name="data">Data.</param>
        /// <param name="hexKey">Key.</param>
        /// <returns>Decoded data.</returns>
        public static string Decode(string data, string hexKey)
        {
            Ensure.ArgumentNotNull(data, nameof(data));
            Ensure.ArgumentNotNull(hexKey, nameof(hexKey));

            return DecodeWorker.DecodeValue(data, hexKey);
        }
    }
}
