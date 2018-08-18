using System;
using System.Linq.Expressions;
using System.Reflection;

namespace MarkUnit.Classes
{
    public interface IClassTestCondition
    {
        IClassRule Be(Expression<Predicate<Type>> typeFilterExpression);
        IClassRule BeDeclaredInNamespaceMatching(string pattern);
        IClassRule BeInAssembly(Assembly assembly);
        IClassRule BeInAssembly(Expression<Predicate<Assembly>> assemblyFilterExpression);
        IClassRule BeInAssemblyMatching(string pattern);
        IClassRule HaveCyclicDependencies();
        IClassRule HaveMethods(Expression<Predicate<IMethod>> methodPredicate);
        IClassRule HaveName(Expression<Predicate<string>> nameFunc);
        IClassRule HaveNameMatching(string pattern);
        IClassRule ImplementInterface<T>();

        IInterfacePredicate ImplementInterface();
        IClassRule ImplementInterfaceMatching(string pattern);
        IClassTestCondition Not();

        IClassRule ReferenceNamespacesMatching(string pattern);
        IClassPredicateEx UseAClass();
        IClassRule UsesClassMatching(string regExOnClassName, string regExreplace);
    }
}
