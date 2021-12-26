namespace ChordAnalyzer
{
    public class Keymap
    {
        public Key[] Keys { get; set; } = { };
        public int[][][] Positions { get; set; } = { };

        public class Key
        {
            public int Row { get; set; }
            public int Column { get; set; }
        }
    }
}