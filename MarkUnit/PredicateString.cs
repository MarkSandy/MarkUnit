using System.Collections.Generic;

namespace MarkUnit
{
    public static class PredicateString
    {
        private static readonly List<string> _warnings = new List<string>();
        public static bool Failed { get; set; }
        public static string Text { get; private set; }
        public static IEnumerable<string> Warnings => _warnings;

        public static void Add(string word)
        {
            Text += " " + word;
        }

        public static void AddWarning(string warningMessage)
        {
            _warnings.Add(warningMessage);
        }

        public static void Start(string prefix)
        {
            Text = prefix;
            Failed = false;
            _warnings.Clear();
        }
    }
}
