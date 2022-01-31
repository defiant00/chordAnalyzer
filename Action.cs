namespace ChordAnalyzer
{
    public class Action
    {
        public int[] Keys { get; set; } = { };

        public override string ToString() => $"[{string.Join(", ", Keys)}]";
    }
}