using System;
using System.Linq.Expressions;
using System.Reflection;

namespace MarkUnit.Classes
{
    public interface ITypeTestCondition
    {
        ITypeRule HaveName(Expression<Predicate<string>> nameFunc);
        ITypeRule HaveNameMatching(string pattern);
        
        ITypeTestCondition Not();

        ITypeRule BeInAssemblyMatching(string pattern);
        ITypeRule BeInAssembly(Assembly assembly);
        ITypeRule BeDeclaredInNamespaceMatching(string pattern);
        ITypeRule Be(Expression<Predicate<Type>> typeFilterExpression);
    }
}