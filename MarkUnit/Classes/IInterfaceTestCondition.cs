using System;
using System.Linq.Expressions;
using System.Reflection;

namespace MarkUnit.Classes
{
    public interface IInterfaceTestCondition
    {
        IInterfaceRule HaveName(Expression<Predicate<string>> nameFilterExpression);
        IInterfaceRule HaveNameMatching(string pattern);
        IInterfaceRule ImplementInterface<TInterface>();
        IInterfaceRule Be(Expression<Predicate<Type>> typeExpressionExpression);
        IInterfaceRule BeInAssembly(Expression<Predicate<Assembly>> predicateExpression);
        IInterfaceRule BeInAssembly(Assembly assembly);
        IInterfaceRule BeInAssemblyMatching(string pattern);
        IInterfacePredicate ImplementInterface();
        IInterfaceRule ImplementInterfaceMatching(string pattern);
        IInterfaceRule BeDeclaredInNamespaceMatching(string pattern);
        IInterfaceRule ReferenceNamespacesMatching(string pattern);
    }
}
