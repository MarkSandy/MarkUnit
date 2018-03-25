using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace MarkUnit.Classes
{
    internal class InterfacePredicate : IInterfacePredicate
    {
        private readonly AssertionVerifier<IClass> _verifier;

        public InterfacePredicate(AssertionVerifier<IClass> verifier)
        {
            _verifier = verifier;
        }

        public IClassMatchingInterfaceCondition That()
        {
            PredicateString.Add("that");
            return  new ClassMatchingInterfaceCondition(new ClassToInterfaceFilterMapper(_verifier.Items),false);
        }
    }

    class ClassToInterfaceFilterMapper : IFilter<IInterface>
    {
        private readonly IFilter<IClass> _filter;
        public IEnumerable<IInterface> FilteredItems => _filter.FilteredItems.Select(i => (IInterface) i);

        public ClassToInterfaceFilterMapper(IFilter<IClass> filter)
        {
            _filter = filter;
        }
        
        public void AppendCondition(Predicate<IInterface> func)
        {
            _filter.AppendCondition(c=>func((IInterface)c));
        }

        public void Materialize()
        {
            _filter.Materialize();
        }

        public void Negate()
        {
            _filter.Negate();
        }
    }

    internal class InterfaceLogicalLink : LogicalLink<IInterfaceTestCondition>, IInterfaceRule 
    {
        private readonly IInternalInterfaceTestCondition _followUp;

        public InterfaceLogicalLink(IInternalInterfaceTestCondition followUp) : base(followUp)
        {
            _followUp = followUp;
        }

        public void Check()
        {
            _followUp.Check(); 
        }
    }


    internal class InterfaceRule : RuleBase<IInterface, IInterfaceTestCondition, IInterfaceRule>, IInternalInterfaceTestCondition
    {
        public InterfaceRule(IFilter<IInterface> items, bool negateAssertion) : base(items, negateAssertion)
        {
            LogicalLink = new InterfaceLogicalLink(this);
        }

        public IInterfaceRule HasName(Expression<Predicate<string>> nameFilterExpression)
        {
            PredicateString.Add($"has name ({nameFilterExpression})");
            var nameFilter = nameFilterExpression.Compile();
              return  AppendCondition(c => nameFilter(c.Name));
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
            return AppendCondition(c => c.Assembly.Name.Matches(pattern));
        }

        public IInterfaceRule IsDeclaredInAssembly(Expression<Predicate<Assembly>> assemblyMatchingExpression)
        {
            PredicateString.Add($"is declared in assembly '{assemblyMatchingExpression}'");
            var assemblyMatcher = assemblyMatchingExpression.Compile();
            return AppendCondition(c => assemblyMatcher(c.Assembly.Assembly));
        }

        public IInterfaceRule Is(Expression<Predicate<Type>> typeExpression)
        {
            PredicateString.Add($"is declared in assembly '{typeExpression}'");
            var typeMatcher = typeExpression.Compile();
            return AppendCondition(c => typeMatcher(c.ClassType));
        }
    }
}