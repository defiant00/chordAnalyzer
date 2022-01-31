namespace ChordAnalyzer
{
    public class ResultState
    {
        public decimal Weight { get; set; }
        public List<string> Reasons { get; set; } = new();

        public override string ToString() => $"({Weight}) [{string.Join(", ", Reasons)}]";
    }
}