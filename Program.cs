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

            var englishBigrams = new List<Bigram>();
            using (var reader = new StreamReader("data\\bigrams\\english.txt"))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                    englishBigrams.Add(new Bigram(line));
            }

            var codeBigrams = new List<Bigram>();
            using (var reader = new StreamReader("data\\bigrams\\code.txt"))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                    codeBigrams.Add(new Bigram(line));
            }

            Console.WriteLine("done");

            foreach (string file in args)
            {
                Console.Write($"Loading {file}...");
                var layout = JsonSerializer.Deserialize<Layout>(File.ReadAllText($"data\\layouts\\{file}.json"));
                if (layout == null)
                {
                    Console.WriteLine($"Layout {file} could not be loaded.");
                    return;
                }
                layout.Compile();
                var keymap = JsonSerializer.Deserialize<Keymap>(File.ReadAllText($"data\\keymaps\\{layout.Keymap}.json"));
                if (keymap == null)
                {
                    Console.WriteLine($"Keymap {layout.Keymap} could not be loaded.");
                    return;
                }
                Console.WriteLine("done");

                decimal weight = CalcWeight(layout, keymap, "a", "b");
                Console.WriteLine($"Weight: {weight}");

                // Analyze(layout, keymap, "English", englishBigrams);
                // Analyze(layout, keymap, "Code", codeBigrams);
            }
        }

        private static void Analyze(Layout layout, Keymap keymap, string bigramName, List<Bigram> bigrams)
        {
            decimal score = 0;
            decimal total = 0;
            foreach (var bigram in bigrams)
            {
                score += CalcWeight(layout, keymap, bigram.Value[0].ToString(), bigram.Value[1].ToString());
                total += bigram.Count;
            }
            score = score * 100 / total;
            Console.WriteLine($"{bigramName} score: {score}");
        }

        private static decimal CalcWeight(Layout layout, Keymap keymap, params string[] items)
        {
            // char first = bigram.Value[0];
            // var actions = new List<List<Layout.Action>>();
            // foreach (var layer in layout.Layers.Where(l => l.ContainsKey(first.ToString())))
            // {
            //     var l = new List<Layout.Action>();
            //     l.AddRange(layer[first.ToString()]);
            //     actions.Add(l);
            // }
            // var mapped = GetMapped(layout, first);
            // if (mapped.Count > 0)
            //     actions.Add(mapped);
            // if (actions.Count == 0)
            //     Console.WriteLine($" ** {layout.Name} does not contain a chord for '{first}'.");

            return 0;
        }

        private static List<Layout.Action> GetMapped(Layout layout, char val)
        {
            // TODO - build the action list recursively so that any number of possible
            // actions works and then all possible combinations are evaluated and the
            // lowest overall score is picked.

            var res = new List<Layout.Action>();
            if (ShiftMapper.Map.ContainsKey(val))
            {
                char mapVal = ShiftMapper.Map[val];
                var layer = layout.Layers.FirstOrDefault(l => l.ContainsKey("shift"));
                if (layer != null)
                    res.AddRange(layer["shift"]);
                layer = layout.Layers.FirstOrDefault(l => l.ContainsKey(mapVal.ToString()));
                if (layer != null)
                    res.AddRange(layer[mapVal.ToString()]);
            }
            return res;
        }
    }
}
