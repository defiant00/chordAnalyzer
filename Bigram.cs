namespace ChordAnalyzer
{
    public class Bigram
    {
        public string Value { get; set; }
        public decimal Count { get; set; }

        public Bigram(string line)
        {
            string[] splits = line.Split('\t');
            if (splits.Length > 1)
            {
                Value = splits[0];
                Count = Convert.ToDecimal(splits[1]);
            }
            else
                Value = string.Empty;
        }
    }
}