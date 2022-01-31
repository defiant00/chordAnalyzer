﻿using System.Text.Json;

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
                var keys = JsonSerializer.Deserialize<Key[]>(File.ReadAllText($"data\\keymaps\\{layout.Keymap}.json"));
                if (keys == null)
                {
                    Console.WriteLine($"Keymap {layout.Keymap} could not be loaded.");
                    return;
                }
                Console.WriteLine("done");

                // ResultState results = CalcResult(layout, keys, "a", "b");
                // Console.WriteLine($"(a, b): {results.Weight} [{string.Join(", ", results.Reasons)}]");
                // results = CalcResult(layout, keys, "A", "b");
                // Console.WriteLine($"(A, b): {results.Weight} [{string.Join(", ", results.Reasons)}]");
                // results = CalcResult(layout, keys, "A", "A");
                // Console.WriteLine($"(A, A): {results.Weight} [{string.Join(", ", results.Reasons)}]");

                Analyze(layout, keys, "English", englishBigrams);
            }
        }

        private static void Analyze(Layout layout, Key[] keys, string bigramName, List<Bigram> bigrams){
            decimal total = 0;
            foreach(var bigram in bigrams){
                var res = CalcResult(layout, keys, bigram.Value[0].ToString(), bigram.Value[1].ToString());
                total += res.Weight * bigram.Count;                
            }
            Console.WriteLine($"{bigramName} score: {total}");
        }

        private static ResultState CalcResult(Layout layout, Key[] keys, params string[] items)
        {
            // generate all possible shifted and unshifted items
            var steps = new List<List<List<string>>>();
            foreach (string item in items)
            {
                var step = new List<List<string>> { new List<string> { item } };
                if (Helpers.ShiftMap.ContainsKey(item))
                    step.Add(new List<string> { Helpers.SHIFT, Helpers.ShiftMap[item] });
                steps.Add(step);
            }

            // generate all combos and combine the lists
            var combos = new List<List<string>>();
            foreach (var combo in steps.Cartesian())
            {
                var stepList = new List<string>();
                foreach (var itemSteps in combo)
                    stepList.AddRange(itemSteps);
                combos.Add(stepList);
            }

            // Console.WriteLine("combos: " + string.Join(", ", combos.Select(fil => string.Join(" ", fil))));

            var bestResult = CalcIndividualResult(layout, keys, combos[0]);
            for (int i = 1; i < combos.Count; i++)
            {
                var result = CalcIndividualResult(layout, keys, combos[i]);
                if ((result.Weight < bestResult.Weight || bestResult.Weight == 0) && result.Weight > 0)
                    bestResult = result;
            }

            return bestResult;
        }

        private static ResultState CalcIndividualResult(Layout layout, Key[] keys, List<string> items)
        {
            string? missing = items.FirstOrDefault(i => !layout.Chords.ContainsKey(i));
            if (missing != null)
            {
                var rs = new ResultState();
                rs.Reasons.Add($"'{missing}' not present in layout.");
                return rs;
            }

            var state = CalcState(layout, keys, items[0]);
            for (int i = 1; i < items.Count; i++)
                state = CalcState(layout, keys, items[i], state);

            return state;
        }

        private static ResultState CalcState(Layout layout, Key[] keys, string item, ResultState? prior = null)
        {
            var state = prior ?? new ResultState();
            var action = layout.Chords[item];
            decimal totalKeyWeight = action.Keys.Sum(k => keys[k].Weight);
            decimal comboWeight = Helpers.ComboWeight[action.Keys.Length];
            state.Reasons.Add(action.Keys.Length == 1 ?
                $"r{keys[action.Keys[0]].Row}c{keys[action.Keys[0]].Column} {keys[action.Keys[0]].Weight}" :
                $"combo_{comboWeight}({string.Join(", ", action.Keys.Select(k => $"r{keys[k].Row}c{keys[k].Column} {keys[k].Weight}"))})"
            );
            state.Weight += (totalKeyWeight * comboWeight);
            return state;
        }
    }
}