namespace ChordAnalyzer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Chord Analyzer v0.0.1");
            Console.WriteLine();

            var keymap = new Keymap();
            keymap.Grid4x2();
        }
    }
}