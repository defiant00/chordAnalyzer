// § - space
// ¬ - tab
// ¶ - enter

namespace ChordAnalyzer
{
    public static class Helpers
    {
        // Shift constant
        public const string SHIFT = "shift";

        // Finger names
        public static string[] FingerMap { get; } =
        {
            "Pinky",
            "Ring",
            "Middle",
            "Index",
            "Thumb",
        };

        // Chord multiplier based off of the number of keys held.
        // Formula: sum(KeyWeights) * ChordWeight
        public static decimal[] ChordWeight { get; } = { 0m, 1m, 0.6m, 1m, 1m, 1m };

        // Per-finger penalty for using the same finger in a row.
        public static decimal[] SameFingerPenalty { get; } = { 1m, 1m, 0.7m, 0.5m, 0.8m };

        // Per-finger movement weight.
        public static decimal[] FingerMovementWeight { get; } = { 1m, 1m, 0.8m, 0.6m, 1m };

        public static Dictionary<string, string> ShiftMap { get; } = new()
        {
            ["A"] = "a",
            ["B"] = "b",
            ["C"] = "c",
            ["D"] = "d",
            ["E"] = "e",
            ["F"] = "f",
            ["G"] = "g",
            ["H"] = "h",
            ["I"] = "i",
            ["J"] = "j",
            ["K"] = "k",
            ["L"] = "l",
            ["M"] = "m",
            ["N"] = "n",
            ["O"] = "o",
            ["P"] = "p",
            ["Q"] = "q",
            ["R"] = "r",
            ["S"] = "s",
            ["T"] = "t",
            ["U"] = "u",
            ["V"] = "v",
            ["W"] = "w",
            ["X"] = "x",
            ["Y"] = "y",
            ["Z"] = "z",

            ["~"] = "`",
            ["!"] = "1",
            ["@"] = "2",
            ["#"] = "3",
            ["$"] = "4",
            ["%"] = "5",
            ["^"] = "6",
            ["&"] = "7",
            ["*"] = "8",
            ["("] = "9",
            [")"] = "0",
            ["_"] = "-",
            ["+"] = "=",

            ["{"] = "[",
            ["}"] = "]",
            ["|"] = "\\",

            [":"] = ";",
            ["\""] = "'",

            ["<"] = ",",
            [">"] = ".",
            ["?"] = "/",
        };

        public static IEnumerable<IEnumerable<T>> Cartesian<T>(this IEnumerable<IEnumerable<T>> vals) =>
            vals.Aggregate((IEnumerable<IEnumerable<T>>)new[] { Enumerable.Empty<T>() },
                (acc, seq) => acc.SelectMany(a => seq.Select(s => a.Append(s))));
    }
}