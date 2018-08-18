using System;
using System.Linq.Expressions;
using System.Reflection;

namespace MarkUnit.Classes
{
    public interface IInterfaceTestCondition
    {
        IInterfaceRule Be(Expression<Predicate<Type>> typeExpressionExpression);
        IInterfaceRule BeDeclaredInNamespaceMatching(string pattern);
        IInterfaceRule BeInAssembly(Expression<Predicate<Assembly>> predicateExpression);
        IInterfaceRule BeInAssembly(Assembly assembly);
        IInterfaceRule BeInAssemblyMatching(string pattern);
        IInterfaceRule HaveName(Expression<Predicate<string>> nameFilterExpression);
        IInterfaceRule HaveNameMatching(string pattern);
        IInterfaceRule ImplementInterface<TInterface>();
        IInterfacePredicate ImplementInterface();
        IInterfaceRule ImplementInterfaceMatching(string pattern);
        IInterfaceRule ReferenceNamespacesMatching(string pattern);
    }
}
