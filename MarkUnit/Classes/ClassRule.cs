using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace MarkUnit.Classes
{
    internal class ClassRule
        : FunctionalComponentRule<IClass, IClassTestCondition, IClassRule>,
          IInternalClassTestCondition,
          IClassRule
    {
        public ClassRule(IAssertionVerifier<IClass> verifier)
            : base(verifier) { }

        public ClassRule(IFilter<IClass> items, bool negateAssertion)
            : base(items, negateAssertion) { }

        protected override IClassRule LogicalLink => this;

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

        public IClassRule HaveMethods(Expression<Predicate<IMethod>> methodPredicate)
        {
            PredicateString.Add($"have methods with '{methodPredicate}'");
            var methodFilter = methodPredicate.Compile();
            // Achtung! Hier könnte sich ein Logikproblem verbergen (not)
            return AppendCondition(c => c.Methods.All(m => methodFilter(m)));
        }

        public IClassRule HaveCyclicDependencies()
        {
            PredicateString.Add("have cyclic dependencies");
            var cyclicDependencyChecker = new CyclicDependencyChecker();
            return AppendCondition(c => cyclicDependencyChecker.HasCyclicDependencies(c));
        }

        bool MatchesName(string name, string regExOnClassName, string regExOnMatchingClass)
        {
            string repl = Regex.Replace(name, regExOnClassName, regExOnMatchingClass);
            return name.Matches(repl);
        }
    }
}
