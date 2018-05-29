using System;
using System.Linq.Expressions;
using System.Reflection;

namespace MarkUnit.Classes
{
    public interface IClassCollection : ICondition<IClassCollection>
    {
        IReducedClassCollection HasAttribute<TAttribute>()
            where TAttribute : Attribute;

        IReducedClassCollection HasName(Expression<Predicate<string>> nameFilter);
        IReducedClassCollection HasNameMatching(string pattern);
        IReducedClassCollection ImplementsInterface<TInterface>();
        IReducedClassCollection ImplementsInterfaceMatching(string pattern);
        IReducedClassCollection Is(Expression<Predicate<Type>> typeExpression);
        IReducedClassCollection IsDeclaredInAssembly(Assembly assembly);
        IReducedClassCollection IsDeclaredInAssembly(string name);
        IReducedClassCollection IsDeclaredInAssembly(Expression<Predicate<Assembly>> predicate);
        IReducedClassCollection IsDeclaredInAssemblyMatching(string pattern);

        IReducedClassCollection IsDerivedFrom<TClass>()
            where TClass : class;
    }

    public interface ITypeCollection : ICondition<ITypeCollection>
    {
        IReducedTypeCollection HasAttribute<TAttribute>()
            where TAttribute : Attribute;
        IReducedTypeCollection IsEnum();
        IReducedTypeCollection IsInterface();
        IReducedTypeCollection ImplementsInterface<TInterface>();
        IReducedTypeCollection ImplementsInterfaceMatching(string pattern);

        IReducedTypeCollection HasName(Expression<Predicate<string>> nameFilter);
        IReducedTypeCollection HasNameMatching(string pattern);
        IReducedTypeCollection Is(Expression<Predicate<Type>> typeExpression);
        IReducedTypeCollection IsDeclaredInAssembly(Assembly assembly);
        IReducedTypeCollection IsDeclaredInAssembly(string name);
        IReducedTypeCollection IsDeclaredInAssembly(Expression<Predicate<Assembly>> predicate);
        IReducedTypeCollection IsDeclaredInAssemblyMatching(string pattern);

        IReducedTypeCollection IsDerivedFrom<TClass>() where TClass : class;
    }
}
