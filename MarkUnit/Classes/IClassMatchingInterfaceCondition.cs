using System;
using System.Linq;
using System.Linq.Expressions;

namespace MarkUnit.Classes
{
    public interface IClassMatchingInterfaceCondition : IInterfaceTestCondition
    {
        IInterfaceRule HasMatchingName(Expression<Func<Type, string>> typeFilterExpression);
    }

    internal class ClassMatchingInterfaceCondition : InterfaceRule, IClassMatchingInterfaceCondition
    {
        public ClassMatchingInterfaceCondition(IFilter<IInterface> items, bool negateAssertion) 
            : base(items, negateAssertion)
        {
        }

        public IInterfaceRule HasMatchingName(Expression<Func<Type, string>> typeFilterExpression)
        {
            PredicateString.Add($"has matching name");
            var classNameMatcher = typeFilterExpression.Compile();
            return AppendCondition(c => c.ClassType.GetInterfaces().Any(i => i.Name.Matches(classNameMatcher(c.ClassType)))); 
        }
    }
}