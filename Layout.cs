namespace ChordAnalyzer
{
    public class Layout
    {
        // The name of the layout.
        public string Name { get; set; } = string.Empty;
        // The name of the keymap.
        public string Keymap { get; set; } = string.Empty;
        // Dictionary of key -> actions that can produce that key.
        public Dictionary<string, Action[]>[] Layers { get; set; } = { };

        public class Action
        {
            // Held key indices.
            public int[] Keys { get; set; } = { };
            // Whether the action is a hold.
            public bool Hold { get; set; }
        }
    }
}