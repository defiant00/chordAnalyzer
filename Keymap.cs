namespace ChordAnalyzer
{
    public class Keymap
    {
        public Key[] Keys { get; set; } = { };
        public int[][][] Positions = { };

        public void Grid4x2()
        {
            Keys = new[] {
                new Key { Row = 0, Column = 0 },
                new Key { Row = 0, Column = 1 },
                new Key { Row = 0, Column = 2 },
                new Key { Row = 0, Column = 3 },
                new Key { Row = 1, Column = 0 },
                new Key { Row = 1, Column = 1 },
                new Key { Row = 1, Column = 2 },
                new Key { Row = 1, Column = 3 },
            };

            // hand positions
            Positions = new[] {
                // finger positions
                new[] {
                    // key indexes per finger
                    new[] {0, 4},   // pinky
                    new[] {1, 5},   // ring
                    new[] {2, 6},   // middle
                    new[] {3, 7},   // index
                    new int[] {},   // thumb
                }
            };
        }

        public class Key
        {
            public int Row { get; set; }
            public int Column { get; set; }
        }
    }
}