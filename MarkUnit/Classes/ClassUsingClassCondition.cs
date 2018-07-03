using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace MarkUnit.Classes
{
    internal class ClassUsingClassCondition
        : RuleBase<IClass, IClassUsesClassCondition, IClassUsingClassRule>,
            IInternalClassUsesClassCondition
    {
        public ClassUsingClassCondition(IAssertionVerifier<IClass> verifier) : base(verifier)
        {
            LogicalLink = new ClassUsingClassLogicalLink(this);
        }

        public ClassUsingClassCondition(IFilter<IClass> items, bool negateAssertion)
            : base(items, negateAssertion)
        {
            LogicalLink = new ClassUsingClassLogicalLink(this);
        }
        
        private IClassUsingClassRule InnerAppendCondition(Func<IClass,Type,bool> interfaceFilter)
        {
            return AppendCondition(c => c.ClassType.GetInterfaces().Any(i => interfaceFilter(c,i)));
        }

        public IClassUsingClassRule HasName(Expression<Predicate<string>> nameFilterExpression)
        {
            PredicateString.Add($"has name matching '{nameFilterExpression}'");
            var nameFilter = nameFilterExpression.Compile();
            return InnerAppendCondition((c,i)=> nameFilter(i.Name));
        }

        public IClassUsingClassRule HasNameMatching(string pattern)
        {
            PredicateString.Add($"has name matching '{pattern}'");
            return InnerAppendCondition((c,i)=> i.Name.Matches(pattern));
        }

        public IClassUsingClassRule ImplementsInterface<TInterface>()
        {
            PredicateString.Add($"implements '{typeof(TInterface)}");
            return InnerAppendCondition((c, i) => typeof(TInterface).IsAssignableFrom(i));
        }

        public IClassUsingClassRule Is(Expression<Predicate<Type>> typeExpressionExpression)
        {
            PredicateString.Add($"is '{typeExpressionExpression}'");
            var typeFilter = typeExpressionExpression.Compile();
            return InnerAppendCondition((c,i)=> typeFilter(i));
        }

        public IClassUsingClassRule IsDeclaredInAssembly(Expression<Predicate<Assembly>> predicateExpression)
        {
            PredicateString.Add($"is declared in an assembly matching '{predicateExpression}'");
            var predicate = predicateExpression.Compile();
            return InnerAppendCondition((c,i)=>  predicate(i.Assembly));
        }

        public IClassUsingClassRule IsDeclaredInAssemblyMatching(string pattern)
        {
            PredicateString.Add($"is declared in an assembly '{pattern}'");
            return InnerAppendCondition((c,i)=> i.Assembly.FullName.Matches(pattern));
        }

        public void AddIgnoreList(string[] exceptions)
        {
            throw new NotImplementedException();
        }
    }
}