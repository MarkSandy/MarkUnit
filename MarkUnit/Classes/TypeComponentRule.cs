using System;
using System.Linq.Expressions;
using System.Reflection;

namespace MarkUnit.Classes
{
    internal abstract class TypeComponentRule<TType, TTest, TRule>
        : RuleBase<TType, TTest, TRule> where TType : IType, INamedComponent where TRule : IRule<TTest>
    {
        public TRule BeInAssembly(Assembly assembly)
        {
            PredicateString.Add($"be in assembly '{assembly.FullName}'");
            return AppendCondition(c => c.Assembly.Assembly.FullName == assembly.FullName);
        }

        public TRule BeInAssemblyMatching(string pattern)
        {
            PredicateString.Add($"be declared in an assembly matching '{pattern}'");
            return AppendCondition(c => c.Assembly.Name.Matches(pattern));
        }

        public TRule Be(Expression<Predicate<Type>> typeExpression)
        {
            PredicateString.Add($"be '{typeExpression}'");
            var typeMatcher = typeExpression.Compile();
            return AppendCondition(c => typeMatcher(c.ClassType));
        }

        public TRule BeDeclaredInNamespaceMatching(string pattern)
        {
            PredicateString.Add($"be declared in namespace matching '{pattern}'");
            return AppendCondition(c => c.Namespace.Matches(pattern));
        }

        protected TypeComponentRule(IFilter<TType> items, bool negateAssertion) : base(items, negateAssertion)
        {
        }

        protected TypeComponentRule(IAssertionVerifier<TType> verifier) : base(verifier)
        {
        }
    }
}