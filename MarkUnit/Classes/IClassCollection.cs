using System;
using System.Linq.Expressions;

namespace MarkUnit.Classes
{
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
}