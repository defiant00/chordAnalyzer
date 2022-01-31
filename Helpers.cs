// § - space
// ¬ - tab
// ¶ - enter

namespace ChordAnalyzer
{
    public static class Helpers
    {
        public const string SHIFT = "shift";

        public static string[] FingerMap { get; } =
        {
            "Pinky",
            "Ring",
            "Middle",
            "Index",
            "Thumb",
        };

        public static decimal[] ComboWeight { get; } = { 0m, 1m, 0.6m, 1m, 1m, 1m };

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