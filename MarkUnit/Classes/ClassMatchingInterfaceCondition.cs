﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace MarkUnit.Classes
{
    internal class ClassMatchingInterfaceCondition
        : RuleBase<IInterface, IClassMatchingInterfaceCondition, IClassMatchingInterfaceRule>,
          IInternalClassMatchingInterfaceCondition,
          IClassMatchingInterfaceRule
    {
        public ClassMatchingInterfaceCondition(IAssertionVerifier<IInterface> verifier)
            : base(verifier) { }

        public ClassMatchingInterfaceCondition(IFilter<IInterface> items, bool negateAssertion)
            : base(items, negateAssertion) { }

        protected override IClassMatchingInterfaceRule LogicalLink => this;

        public IClassMatchingInterfaceRule HasMatchingClassName()
        {
            PredicateString.Add($"has matching name");
            return InnerHasMatchingClassName("^(.*)$", "I$1");
        }

        public IClassMatchingInterfaceRule HasMatchingClassName(string regExClass, string matchingInterfaceNameRegEx)
        {
            PredicateString.Add($"has a name matching the implementation ('{regExClass}' => '{matchingInterfaceNameRegEx}'");
            return InnerHasMatchingClassName(regExClass, matchingInterfaceNameRegEx);
        }

        public IClassMatchingInterfaceRule HasMatchingName(Expression<Func<Type, string>> typeFilterExpression)
        {
            PredicateString.Add($"has matching name {typeFilterExpression}");
            var classNameMatcher = typeFilterExpression.Compile();
            return InnerAppendCondition((c, i) => i.Name.Matches(classNameMatcher(c.ClassType)));
        }

        public IClassMatchingInterfaceRule HasName(Expression<Predicate<string>> nameFilterExpression)
        {
            PredicateString.Add($"has name matching '{nameFilterExpression}'");
            var nameFilter = nameFilterExpression.Compile();
            return InnerAppendCondition((c, i) => nameFilter(i.Name));
        }

        public IClassMatchingInterfaceRule HasNameMatching(string pattern)
        {
            PredicateString.Add($"has name matching '{pattern}'");
            return InnerAppendCondition((c, i) => i.Name.Matches(pattern));
        }

        public IClassMatchingInterfaceRule ImplementsInterface<TInterface>()
        {
            PredicateString.Add($"implements '{typeof(TInterface)}");
            return InnerAppendCondition((c, i) => typeof(TInterface).IsAssignableFrom(i));
        }

        public IClassMatchingInterfaceRule Is(Expression<Predicate<Type>> typeExpressionExpression)
        {
            PredicateString.Add($"is '{typeExpressionExpression}'");
            var typeFilter = typeExpressionExpression.Compile();
            return InnerAppendCondition((c, i) => typeFilter(i));
        }

        public IClassMatchingInterfaceRule IsDeclaredInAssembly(Expression<Predicate<Assembly>> predicateExpression)
        {
            PredicateString.Add($"is declared in an assembly matching '{predicateExpression}'");
            var predicate = predicateExpression.Compile();
            return InnerAppendCondition((c, i) => predicate(i.Assembly));
        }

        public IClassMatchingInterfaceRule IsDeclaredInAssemblyMatching(string pattern)
        {
            PredicateString.Add($"is declared in an assembly '{pattern}'");
            return InnerAppendCondition((c, i) => i.Assembly.FullName.Matches(pattern));
        }

        private IClassMatchingInterfaceRule InnerAppendCondition(Func<IClass, Type, bool> interfaceFilter)
        {
            return AppendCondition(c => c.ClassType.GetInterfaces().Any(i => interfaceFilter(c, i)));
        }

        private IClassMatchingInterfaceRule InnerHasMatchingClassName(string regExClass, string matchingInterfaceNameRegEx)
        {
            return InnerAppendCondition((c, i) => Tester(regExClass, matchingInterfaceNameRegEx, i, c));
        }

        private static bool Tester(string regExClass, string matchingInterfaceNameRegEx, Type i, IClass c)
        {
            string replace;
            if (c.Name.Contains("FertigungSearch"))
                replace = Regex.Replace(c.Name, regExClass, matchingInterfaceNameRegEx);
            else
            {
                replace = Regex.Replace(c.Name, regExClass, matchingInterfaceNameRegEx);
            }

            return i.Name == replace;
        }
    }
}
