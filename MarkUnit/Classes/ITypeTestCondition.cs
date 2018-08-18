using System;
using System.Linq.Expressions;
using System.Reflection;

namespace MarkUnit.Classes
{
    public interface ITypeTestCondition
    {
        ITypeRule Be(Expression<Predicate<Type>> typeFilterExpression);
        ITypeRule BeDeclaredInNamespaceMatching(string pattern);
        ITypeRule BeInAssembly(Assembly assembly);

        ITypeRule BeInAssemblyMatching(string pattern);
        ITypeRule HaveName(Expression<Predicate<string>> nameFunc);
        ITypeRule HaveNameMatching(string pattern);

        ITypeTestCondition Not();
    }
}
