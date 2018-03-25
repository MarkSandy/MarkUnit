using System;
using System.Linq.Expressions;
using System.Reflection;

namespace MarkUnit.Classes
{
    public interface IClassCollection : ICondition<IClassCollection>
    {
        IReducedClassCollection HasName(Expression<Predicate<string>> nameFilter);
        IReducedClassCollection HasNameMatching(string pattern);
        IReducedClassCollection ImplementsInterface<TInterface>();
        IReducedClassCollection IsDerivedFrom<TClass>() where TClass : class;
        IReducedClassCollection IsDeclaredInAssemblyMatching(string pattern);
        IReducedClassCollection IsDeclaredInAssembly(string name);
        IReducedClassCollection IsDeclaredInAssembly(Expression<Predicate<Assembly>> predicate);
        IReducedClassCollection Is(Expression<Predicate<Type>> typeExpression);
        IReducedClassCollection HasAttribute<TAttribute>() where TAttribute : Attribute;
        IReducedClassCollection ImplementsInterfaceMatching(string pattern);
    } 

    public interface IReducedClassCollection : IFilterConditionChain<IClassCollection, IClassTestCondition>{}
}