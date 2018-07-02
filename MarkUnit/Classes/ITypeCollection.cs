using System;
using System.Linq.Expressions;

namespace MarkUnit.Classes
{
    public interface ITypeCollection : ICondition<ITypeCollection>, INamePredicate<IReducedTypeCollection>, IDeclaredInAssemblyPredicate<IReducedTypeCollection>
    {
        IReducedTypeCollection HasAttribute<TAttribute>()
            where TAttribute : Attribute;

        IReducedTypeCollection IsEnum();
        IReducedInterfaceCollection IsInterface(); 
        IReducedClassCollection IsClass();

        IReducedTypeCollection Is(Expression<Predicate<Type>> typeExpression);
    }
}