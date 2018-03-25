using System;
using System.Linq.Expressions;

namespace MarkUnit.Classes
{
    public interface IClassMatchingInterfaceCondition : IInterfaceTestCondition
    {
        IInterfaceRule HasMatchingName(Expression<Func<Type, string>> typeFilterExpression);
    }
}