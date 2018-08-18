using System;
using System.Linq.Expressions;
using System.Reflection;

namespace MarkUnit.Classes
{
    public interface IClassMatchingInterfaceCondition
    {
        IClassMatchingInterfaceRule HasMatchingClassName();
        IClassMatchingInterfaceRule HasMatchingClassName(string regExClass, string matchingInterfaceNameRegEx);
        IClassMatchingInterfaceRule HasMatchingName(Expression<Func<Type, string>> typeFilterExpression);
        IClassMatchingInterfaceRule HasName(Expression<Predicate<string>> nameFilterExpression);
        IClassMatchingInterfaceRule HasNameMatching(string pattern);
        IClassMatchingInterfaceRule ImplementsInterface<TInterface>();
        IClassMatchingInterfaceRule Is(Expression<Predicate<Type>> typeExpressionExpression);
        IClassMatchingInterfaceRule IsDeclaredInAssembly(Expression<Predicate<Assembly>> predicateExpression);
        IClassMatchingInterfaceRule IsDeclaredInAssemblyMatching(string pattern);
    }
}
