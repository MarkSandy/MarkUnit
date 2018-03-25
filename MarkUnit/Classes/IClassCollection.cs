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
        IReducedClassCollection IsDeclaredInAssembly(string name);
        IReducedClassCollection IsDeclaredInAssembly(Expression<Predicate<Assembly>> predicate);
        IReducedClassCollection IsDeclaredInAssemblyMatching(string pattern);

        IReducedClassCollection IsDerivedFrom<TClass>()
            where TClass : class;
    }
}
