using System;
using System.Linq.Expressions;
using System.Reflection;

namespace MarkUnit.Classes
{
    public interface IInterfaceTestCondition
    {
        IInterfaceRule HasName(Expression<Predicate<string>> nameFilterExpression);
        IInterfaceRule HasNameMatching(string pattern);
        IInterfaceRule ImplementsInterface<TInterface>();
        IInterfaceRule Is(Expression<Predicate<Type>> typeExpressionExpression);
        IInterfaceRule IsDeclaredInAssembly(Expression<Predicate<Assembly>> predicateExpression);
        IInterfaceRule IsDeclaredInAssemblyMatching(string pattern);
    }
}
