namespace Des.Models
{
    public class Subkey
    {
        public Subkey(string c, string d, string key)
        {
            C = c;
            D = d;
            Key = key;
        }

        public string C { get; }
        public string D { get; }
        public string Key { get; }
    }
}
