using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace MarkUnit.Classes
{
    internal class ClassMatchingInterfaceCondition
        : RuleBase<IInterface, IClassMatchingInterfaceCondition, IClassMatchingInterfaceRule>,
         IInternalClassMatchingInterfaceCondition
    {
        public ClassMatchingInterfaceCondition(IAssertionVerifier<IInterface> verifier) : base(verifier)
        {
            LogicalLink = new ClassMatchingInterfaceLogicalLink(this);
        }

        public ClassMatchingInterfaceCondition(IFilter<IInterface> items, bool negateAssertion)
            : base(items, negateAssertion)
        {
            LogicalLink = new ClassMatchingInterfaceLogicalLink(this);
        }

        public IClassMatchingInterfaceRule HasMatchingClassName()
        {
            PredicateString.Add($"has matching name");
            return InnerHasMatchingClassName("(.*)^$", "I$1");
        }

        public IClassMatchingInterfaceRule HasMatchingClassName(string regExClass, string matchingInterfaceNameRegEx)
        {
            PredicateString.Add($"has a name matching the implementation ('{regExClass}' => '{matchingInterfaceNameRegEx}'");
            return InnerHasMatchingClassName(regExClass, matchingInterfaceNameRegEx);
        }

        private IClassMatchingInterfaceRule InnerHasMatchingClassName(string regExClass, string matchingInterfaceNameRegEx)
        {
            return InnerAppendCondition((c, i) => i.Name == Regex.Replace(c.Name, regExClass, matchingInterfaceNameRegEx));
        }
        public IClassMatchingInterfaceRule HasMatchingName(Expression<Func<Type, string>> typeFilterExpression)
        {
            PredicateString.Add($"has matching name {typeFilterExpression}");
            var classNameMatcher = typeFilterExpression.Compile();
            return InnerAppendCondition((c,i) => i.Name.Matches(classNameMatcher(c.ClassType)));
        }

        private IClassMatchingInterfaceRule InnerAppendCondition(Func<IClass,Type,bool> interfaceFilter)
        {
            return AppendCondition(c => c.ClassType.GetInterfaces().Any(i => interfaceFilter(c,i)));
        }

        public IClassMatchingInterfaceRule HasName(Expression<Predicate<string>> nameFilterExpression)
        {
            PredicateString.Add($"has name matching '{nameFilterExpression}'");
            var nameFilter = nameFilterExpression.Compile();
            return InnerAppendCondition((c,i)=> nameFilter(i.Name));
        }

        public IClassMatchingInterfaceRule HasNameMatching(string pattern)
        {
            PredicateString.Add($"has name matching '{pattern}'");
            return InnerAppendCondition((c,i)=> i.Name.Matches(pattern));
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
            return InnerAppendCondition((c,i)=> typeFilter(i));
        }

        public IClassMatchingInterfaceRule IsDeclaredInAssembly(Expression<Predicate<Assembly>> predicateExpression)
        {
            PredicateString.Add($"is declared in an assembly matching '{predicateExpression}'");
            var predicate = predicateExpression.Compile();
            return InnerAppendCondition((c,i)=>  predicate(i.Assembly));
        }

        public IClassMatchingInterfaceRule IsDeclaredInAssemblyMatching(string pattern)
        {
            PredicateString.Add($"is declared in an assembly '{pattern}'");
            return InnerAppendCondition((c,i)=> i.Assembly.FullName.Matches(pattern));
        }
    }

    public interface IClassMatchingInterfaceRule :  IRule<IClassMatchingInterfaceCondition>, ICheckable{}

    internal class ClassMatchingInterfaceLogicalLink
        : LogicalLink<IClassMatchingInterfaceCondition>,
            IClassMatchingInterfaceRule
    {
        private readonly IInternalClassMatchingInterfaceCondition _followUp;

        public ClassMatchingInterfaceLogicalLink(IInternalClassMatchingInterfaceCondition followUp) : base(followUp)
        {
            _followUp = followUp;
        }


        public void Check()
        {
            _followUp.Check();
        }
    }
}
