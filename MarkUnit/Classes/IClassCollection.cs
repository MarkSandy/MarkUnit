using System;
using System.Linq.Expressions;
using System.Reflection;

namespace MarkUnit.Classes
{
    public interface IDeclaredInAssemblyPredicate<out TPostCondition>
    {
        TPostCondition IsDeclaredInAssembly(Assembly assembly);
        TPostCondition IsDeclaredInAssembly(string name);
        TPostCondition IsDeclaredInAssembly(Expression<Predicate<Assembly>> predicate);
        TPostCondition IsDeclaredInAssemblyMatching(string pattern);
    }

    public interface IClassCollection : ICondition<IClassCollection>, INamePredicate<IReducedClassCollection>, IDeclaredInAssemblyPredicate<IReducedClassCollection>
    {
        IReducedClassCollection HasAttribute<TAttribute>()
            where TAttribute : Attribute;

        IReducedClassCollection ImplementsInterface<TInterface>();
        IReducedClassCollection ImplementsInterfaceMatching(string pattern);
        IReducedClassCollection Is(Expression<Predicate<Type>> typeExpression);

        IReducedClassCollection IsDerivedFrom<TClass>()
            where TClass : class;
    }

    public interface ITypeCollection : ICondition<ITypeCollection>, INamePredicate<IReducedTypeCollection>, IDeclaredInAssemblyPredicate<IReducedTypeCollection>
    {
        IReducedTypeCollection HasAttribute<TAttribute>()
            where TAttribute : Attribute;

        IReducedTypeCollection IsEnum();
        IReducedTypeCollection IsInterface(); // should return 'IReducedInterfaceCollection' later
        IReducedClassCollection IsClass();

        IReducedTypeCollection Is(Expression<Predicate<Type>> typeExpression);
    }
}