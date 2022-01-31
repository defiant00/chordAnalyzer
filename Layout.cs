namespace ChordAnalyzer
{
    public class Layout
    {
        public string Name { get; set; } = string.Empty;
        public string Keymap { get; set; } = string.Empty;
        public Dictionary<string, Action> Chords { get; set; } = new();
    }
}