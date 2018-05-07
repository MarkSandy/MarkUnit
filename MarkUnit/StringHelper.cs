using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace MarkUnit
{
    public static class StringHelper
    {
        public static bool MatchesRegEx(this string name, string original,string regExOnClassName, string regExOnMatchingClass)
        {
            string repl = Regex.Replace(original, regExOnClassName, regExOnMatchingClass);
            return name.Matches(repl);
        }
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

    public static class TypeHelper
    {
        public static bool ImplementsInterface<TInterface>(this Type type)
        {
                return type.GetInterfaces().Any(i => i.GUID == typeof(TInterface).GUID);
                // return typeof(TInterface).IsAssignableFrom(c.ClassType); Does not work here
        }
    }
}