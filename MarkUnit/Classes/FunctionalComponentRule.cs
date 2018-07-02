using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace MarkUnit.Classes
{
    internal abstract class FunctionalComponentRule<TType, TTest,TRule>  
        : TypeComponentRule<TType, TTest, TRule> where TType : IClass,INamedComponent where TRule : IRule<TTest>
    {
        protected FunctionalComponentRule(IFilter<TType> items, bool negateAssertion) : base(items, negateAssertion)
        {
        }

        protected FunctionalComponentRule(IAssertionVerifier<TType> verifier) : base(verifier)
        {
        }

        public IInterfacePredicate ImplementInterface()
        {
            PredicateString.Add($"implement interface");
            return new InterfacePredicate<TType>(Verifier);
        }

        public TRule ImplementInterfaceMatching(string pattern)
        {
            PredicateString.Add($"implement an interface matching '{pattern}'");
            return AppendCondition(c => c.ClassType.GetInterfaces().Any(i => StringHelper.Matches(i.Name, pattern)));
        }

        public TRule ImplementInterface<TInterface>()
        {
            PredicateString.Add($"implements {typeof(TInterface).Name}");
            return AppendCondition(c => typeof(TInterface).IsAssignableFrom(c.ClassType));
        }


        public TRule ReferenceNamespacesMatching(string pattern)
        {
            PredicateString.Add($"reference a namespace matching '{pattern}'");
            return AppendCondition(c => c.ReferencedNameSpaces.Any(n => n.Matches(pattern)));
        }

        public TRule BeInAssembly(Expression<Predicate<Assembly>> assemblyMatchingExpression)
        {
            PredicateString.Add($"be declared in assembly '{assemblyMatchingExpression}'");
            var assemblyMatcher = assemblyMatchingExpression.Compile();
            return AppendCondition(c => Tester(assemblyMatcher, c));
        }

        private static bool Tester(Predicate<Assembly> assemblyMatcher, TType c)
        {
            var r=assemblyMatcher(c.Assembly.Assembly);
            return r;
        }

        public TRule Be(Expression<Predicate<Type>> typeExpression)
        {
            PredicateString.Add($"be '{typeExpression}'");
            var typeMatcher = typeExpression.Compile();
            return AppendCondition(c => typeMatcher(c.ClassType));
        }
    }
}