using System.Text.Json;

namespace ChordAnalyzer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Chord Analyzer v0.1.0");
            Console.WriteLine();

            if (args.Length == 0)
            {
                Console.WriteLine("Specify a layout to analyze.");
                return;
            }

            Console.Write("Loading bigrams...");

            var codeBigrams = new List<Bigram>();
            using (var reader = new StreamReader("data\\bigrams\\code.txt"))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                    codeBigrams.Add(new Bigram(line));
            }

            var englishBigrams = new List<Bigram>();
            using (var reader = new StreamReader("data\\bigrams\\english.txt"))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                    englishBigrams.Add(new Bigram(line));
            }

            decimal codeTotal = 0;
            foreach (var b in codeBigrams)
                codeTotal += b.Count;

            decimal englishTotal = 0;
            foreach (var b in englishBigrams)
                englishTotal += b.Count;

            Console.WriteLine("done");

            Console.WriteLine($"Code total: {codeTotal}");
            Console.WriteLine($"English total: {englishTotal}");

            foreach (string layoutFile in args)
            {
                Console.Write($"Loading {layoutFile}...");
                var layout = JsonSerializer.Deserialize<Layout>(File.ReadAllText($"data\\layouts\\{layoutFile}.json"));
                if (layout == null)
                {
                    Console.WriteLine($"Layout {layoutFile} could not be loaded.");
                    return;
                }
                var keymap = JsonSerializer.Deserialize<Keymap>(File.ReadAllText($"data\\keymaps\\{layout.Keymap}.json"));
                if (keymap == null)
                {
                    Console.WriteLine($"Keymap {layout.Keymap} could not be loaded.");
                    return;
                }
                Console.WriteLine("done");
                decimal score = 12;
                Console.WriteLine($"Score: {score}");
            }
        }
    }
}
