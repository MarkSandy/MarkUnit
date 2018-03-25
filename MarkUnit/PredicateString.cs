namespace MarkUnit
{
    public static class PredicateString
    {
        public static bool Failed { get; set; }
        public static string Text { get; private set; }

        public static void Add(string word)
        {
            Text += " " + word;
        }

        public static void Start(string prefix)
        {
            Text = prefix;
            Failed = false;
        }
    }
}
