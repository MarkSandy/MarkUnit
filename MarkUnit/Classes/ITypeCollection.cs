using System;
using System.Linq.Expressions;

namespace MarkUnit.Classes
{
    public interface ITypeCollection
        : ICondition<ITypeCollection>,
          INamePredicate<IReducedTypeCollection>,
          IDeclaredInAssemblyPredicate<IReducedTypeCollection>
    {
        IReducedTypeCollection HasAttribute<TAttribute>()
            where TAttribute : Attribute;

        IReducedTypeCollection Is(Expression<Predicate<Type>> typeExpression);
        IReducedClassCollection IsClass();

        IReducedTypeCollection IsEnum();
        IReducedInterfaceCollection IsInterface();
    }
}
