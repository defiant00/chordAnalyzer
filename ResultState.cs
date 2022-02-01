namespace ChordAnalyzer
{
    public class ResultState
    {
        // Total current weight for the result.
        public decimal Weight { get; set; }
        // List of reasons affecting the weight.
        public List<string> Reasons { get; set; } = new();
        // Prior finger positions.
        public Position?[] PriorPositions { get; set; } = { null, null, null, null, null };

        public override string ToString() => $"({Weight}) [{string.Join(", ", Reasons)}]";
    }
}