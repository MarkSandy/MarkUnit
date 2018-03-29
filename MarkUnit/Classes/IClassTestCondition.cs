using System;
using System.Linq.Expressions;

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
        IClassRule BeDeclaredInNamespaceMatching(string pattern);
        IClassRule Be(Expression<Predicate<Type>> typeFilterExpression);
    }
}
