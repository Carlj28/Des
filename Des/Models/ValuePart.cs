namespace Des.Models
{
    public class ValuePart
    {
        public ValuePart(string value, int index)
        {
            Value = value;
            Index = index;
        }

        public string Value { get; }
        public int Index { get; }
    }
}
