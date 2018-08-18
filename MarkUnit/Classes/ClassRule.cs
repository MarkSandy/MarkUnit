using System;
using System.Collections.Generic;
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

    internal class CyclicDependencyChecker
    {
        private readonly Dictionary<string, bool> _knownDependencies = new Dictionary<string, bool>();

        public bool HasCyclicDependencies(IClass classInfo)
        {
            return ReferencesIndirect(classInfo, classInfo.Namespace);
        }

        private void AddKnownDependency(IClass classInfo, string nameSpace, bool isDependent)
        {
            var key = $"{classInfo.ClassType.FullName}->{nameSpace}";
            if (!_knownDependencies.ContainsKey(key))
            {
                _knownDependencies.Add(key, isDependent);
            }
        }

        private bool IsKnownDependency(IClass classInfo, string nameSpace, out bool result)
        {
            var key = $"{classInfo.ClassType.FullName}->{nameSpace}";
            if (_knownDependencies.TryGetValue(key, out result)) return true;
            result = false;
            return false;
        }

        private bool ReferencesIndirect(IClass classInfo, string nameSpace)
        {
            if (IsKnownDependency(classInfo, nameSpace, out bool result)) return result;

            result = classInfo.ReferencedClasses.Any(c => ReferencesNamespace(c, nameSpace));

            AddKnownDependency(classInfo, nameSpace, result);

            return result;
        }

        private bool ReferencesNamespace(IClass classInfo, string nameSpace)
        {
            return classInfo.Namespace != nameSpace
                   && classInfo.ReferencedNameSpaces.Contains(nameSpace)
                   || ReferencesIndirect(classInfo, nameSpace);
        }
    }
}
