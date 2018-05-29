using System;
using System.Linq.Expressions;
using System.Reflection;

namespace MarkUnit.Classes
{
    public interface IClassTestCondition
    {
        IClassRule HaveName(Expression<Predicate<string>> nameFunc);
        IClassRule HaveNameMatching(string pattern);
        IClassRule ImplementInterface<T>();

        IInterfacePredicate ImplementInterface();
        IClassRule ImplementInterfaceMatching(string pattern);
        IClassTestCondition Not();

        IClassRule ReferenceNamespacesMatching(string pattern);
        IClassRule UsesClassMatching(string regExOnClassName, string regExreplace);
        IClassRule BeInAssemblyMatching(string pattern);
        IClassRule BeInAssembly(Assembly assembly);
        IClassRule BeDeclaredInNamespaceMatching(string pattern);
        IClassRule Be(Expression<Predicate<Type>> typeFilterExpression);
    }


    public interface ITypeTestCondition
    {
        ITypeRule HaveName(Expression<Predicate<string>> nameFunc);
        ITypeRule HaveNameMatching(string pattern);
        ITypeRule ImplementInterface<T>();

        IInterfacePredicate ImplementInterface();
        ITypeRule ImplementInterfaceMatching(string pattern);
        ITypeTestCondition Not();

        ITypeRule ReferenceNamespacesMatching(string pattern);
        ITypeRule UsesClassMatching(string regExOnClassName, string regExreplace);
        ITypeRule BeInAssemblyMatching(string pattern);
        ITypeRule BeInAssembly(Assembly assembly);
        ITypeRule BeDeclaredInNamespaceMatching(string pattern);
        ITypeRule Be(Expression<Predicate<Type>> typeFilterExpression);
    }

    
}
