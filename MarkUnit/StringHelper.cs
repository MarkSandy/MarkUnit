using System;
using System.Linq;

namespace MarkUnit
{
    public static class StringHelper
    {
        public static bool Matches(this string text, string pattern)
        {
            if (string.IsNullOrEmpty(pattern))
            {
                return true;
            }

            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            var start = pattern[0] == '*' ? 1 : 0;
            var end = pattern.LastOrDefault() == '*' ? 1 : 0;
            var p = pattern.Substring(start, pattern.Length - start - end);
            switch (2 * start + end)
            {
                case 0: return string.Equals(text, p, StringComparison.CurrentCultureIgnoreCase);
                case 1: return text.StartsWith(p, StringComparison.CurrentCultureIgnoreCase);
                case 2: return text.EndsWith(p, StringComparison.CurrentCultureIgnoreCase);
                case 3: return text.IndexOf(p, StringComparison.CurrentCultureIgnoreCase) >= 0;
                default: throw new InvalidOperationException("invalid code");
            }
        }
    }
}
