using System;
using System.Linq.Expressions;

namespace MarkUnit.Classes
{
    public interface IInterfaceCollection : ICondition<IInterfaceCollection>, INamePredicate<IReducedInterfaceCollection>, IDeclaredInAssemblyPredicate<IReducedInterfaceCollection>
    {
        IReducedInterfaceCollection HasAttribute<TAttribute>()
            where TAttribute : Attribute;

        IReducedInterfaceCollection ImplementsInterface<TInterface>();
        IReducedInterfaceCollection ImplementsInterfaceMatching(string pattern);
        IReducedInterfaceCollection Is(Expression<Predicate<Type>> typeExpression);
    }
}