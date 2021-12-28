namespace ChordAnalyzer
{
    public class Layout
    {
        // The name of the layout.
        public string Name { get; set; } = string.Empty;
        // The name of the keymap.
        public string Keymap { get; set; } = string.Empty;
        // Dictionary of key -> actions that can produce that key.
        public List<Dictionary<string, List<Action>>> Layers { get; set; } = new();

        public void Compile()
        {
            for (int i = 1; i < Layers.Count; i++)
            {
                // Hashset of all actions currently in the layer
                var present = new HashSet<int>();
                foreach (var actions in Layers[i].Values)
                    foreach (var action in actions)
                        present.Add(action.Id);

                // Go through the prior layer and add those actions
                // to the current one if they're not present.
                foreach (var key in Layers[i - 1].Keys)
                {
                    var actions = Layers[i - 1][key];
                    foreach (var action in actions)
                    {
                        if (!present.Contains(action.Id))
                        {
                            present.Add(action.Id);
                            if (!Layers[i].ContainsKey(key))
                                Layers[i][key] = new List<Action>();
                            Layers[i][key].Add(action);
                        }
                    }
                }
            }
        }

        public class Action
        {
            // Held key indices.
            public int[] Keys { get; set; } = { };
            // Whether the action is a hold.
            public bool Hold { get; set; }

            public int Id
            {
                get
                {
                    int val = 0;
                    foreach (int key in Keys)
                        val += (1 << key);
                    return val;
                }
            }
        }
    }
}