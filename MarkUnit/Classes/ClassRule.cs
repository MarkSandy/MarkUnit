using System.Linq;
using System.Text.RegularExpressions;

namespace MarkUnit.Classes
{
  internal class ClassRule : FunctionalComponentRule<IClass, IClassTestCondition,IClassRule>, IInternalClassTestCondition
    {
        public ClassRule(IAssertionVerifier<IClass> verifier) : base(verifier)
        {
            LogicalLink = new ClassLogicalLink(this);
        }

        public ClassRule(IFilter<IClass> items, bool negateAssertion) : base(items, negateAssertion)
        {
            LogicalLink = new ClassLogicalLink(this);
        }

        public IClassRule UsesClassMatching(string regExOnClassName, string regExOnMatchingClass)
        {
            PredicateString.Add($"uses a class matching the regex expressions('{regExOnClassName}','{regExOnMatchingClass}'");
            return AppendCondition(c => c.ReferencedClasses.Any(x => MatchesName(x.Name, regExOnClassName, regExOnMatchingClass)));
        }

        public IClassPredicateEx UseAClass()
        {
            PredicateString.Add($"use a class");
            return new ClassPredicateEx(Verifier);
        }

        bool MatchesName(string name, string regExOnClassName, string regExOnMatchingClass)
        {
            string repl = Regex.Replace(name, regExOnClassName, regExOnMatchingClass);
            return name.Matches(repl);
        }
    }
}