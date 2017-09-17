namespace Des.Extensions
{
    /// <summary>
    /// Extensions for char and char array
    /// </summary>
    public static class CharExtensions
    {
        /// <summary>
        /// Converts hex char to int
        /// </summary>
        /// <param name="hexChar">Hex char.</param>
        /// <returns>Hex char int value.</returns>
        public static int HexToInt(this char hexChar)
        {
            hexChar = char.ToUpper(hexChar);

            return (int)hexChar < (int)'A' ?
                ((int)hexChar - (int)'0') :
                10 + ((int)hexChar - (int)'A');
        }
    }
}
