using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

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

        public IClassMatchingInterfaceRule HasMatchingName(Expression<Func<Type, string>> typeFilterExpression)
        {
            PredicateString.Add($"has matching name {typeFilterExpression}");
            var classNameMatcher = typeFilterExpression.Compile();
            return InnerAppendCondition((c,i) => i.Name.Matches(classNameMatcher(c.ClassType)));
        }

        private IClassMatchingInterfaceRule InnerAppendCondition(Func<IClassInfo,Type,bool> interfaceFilter)
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

        public IClassMatchingInterfaceRule HasMatchingClassName()
        {
            PredicateString.Add($"has a name matching the name of the class (eg. MyClass implements IMyClass)");
         
            return InnerMatchesClassName("(^.*$)", "I$1");
        }
        private IClassMatchingInterfaceRule InnerMatchesClassName(string regExPatternOnClass, string matchingInterfaceRegEx)
        {
              return InnerAppendCondition((c,i)=> i.Name.MatchesRegEx(c.Name,regExPatternOnClass, matchingInterfaceRegEx));
        }

        public IClassMatchingInterfaceRule MatchesClassName(string regExPatternOnClass, string matchingInterfaceRegEx)
        {
            PredicateString.Add($"has a name matching the name of the class ('{regExPatternOnClass}', '{matchingInterfaceRegEx}'");
            return InnerMatchesClassName(regExPatternOnClass,matchingInterfaceRegEx);
        }

        public IClassMatchingInterfaceRule ImplementsInterface<TInterface>()
        {
            PredicateString.Add($"implements '{typeof(TInterface)}");
            return InnerAppendCondition((c, i) => i.ImplementsInterface<TInterface>());
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
