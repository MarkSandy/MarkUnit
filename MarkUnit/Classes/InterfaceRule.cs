using System;
using System.Linq.Expressions;
using System.Reflection;

namespace MarkUnit.Classes
{
    internal class InterfaceRule
        : RuleBase<IInterface, IInterfaceTestCondition, IInterfaceRule>,
          IInternalInterfaceTestCondition
    {
        public InterfaceRule(IFilter<IInterface> items, bool negateAssertion)
            : base(items, negateAssertion)
        {
            LogicalLink = new InterfaceLogicalLink(this);
        }

        public InterfaceRule(IAssertionVerifier<IInterface> verifier) : base(verifier)
        {
            LogicalLink = new InterfaceLogicalLink(this);
        }

        public IInterfaceRule HasName(Expression<Predicate<string>> nameFilterExpression)
        {
            PredicateString.Add($"has name ({nameFilterExpression})");
            var nameFilter = nameFilterExpression.Compile();
            return AppendCondition(c => nameFilter(c.Name));
        }

        public IInterfaceRule HasNameMatching(string pattern)
        {
            PredicateString.Add($"has name matching '{pattern}'");
            return AppendCondition(c => c.Name.Matches(pattern));
        }

        public IInterfaceRule ImplementsInterface<TInterface>()
        {
            PredicateString.Add($"implements {typeof(TInterface).Name}");
            return AppendCondition(c => typeof(TInterface).IsAssignableFrom(c.ClassType));
        }

        public IInterfaceRule IsDeclaredInAssemblyMatching(string pattern)
        {
            PredicateString.Add($"is declared in an assembly matching '{pattern}'");
            return AppendCondition(c => c.AssemblyInfo.Name.Matches(pattern));
        }

        public IInterfaceRule IsDeclaredInAssembly(Expression<Predicate<Assembly>> assemblyMatchingExpression)
        {
            PredicateString.Add($"is declared in assembly '{assemblyMatchingExpression}'");
            var assemblyMatcher = assemblyMatchingExpression.Compile();
            return AppendCondition(c => assemblyMatcher(c.AssemblyInfo.Assembly.Assembly));
        }

        public IInterfaceRule Is(Expression<Predicate<Type>> typeExpression)
        {
            PredicateString.Add($"is declared in assembly '{typeExpression}'");
            var typeMatcher = typeExpression.Compile();
            return AppendCondition(c => typeMatcher(c.ClassType));
        }
    }
}
