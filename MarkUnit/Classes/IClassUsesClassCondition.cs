using System;
using System.Linq.Expressions;
using System.Reflection;

namespace MarkUnit.Classes
{
    public interface IClassUsesClassCondition
    {
        IClassUsingClassRule HasName(Expression<Predicate<string>> nameFilterExpression);
        IClassUsingClassRule HasNameMatching(string pattern);
        IClassUsingClassRule ImplementsInterface<TInterface>();
        IClassUsingClassRule Is(Expression<Predicate<Type>> typeExpressionExpression);
        IClassUsingClassRule IsDeclaredInAssembly(Expression<Predicate<Assembly>> predicateExpression);
        IClassUsingClassRule IsDeclaredInAssemblyMatching(string pattern);
    }
}
