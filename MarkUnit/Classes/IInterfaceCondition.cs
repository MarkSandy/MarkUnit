using System;
using System.Linq.Expressions;
using System.Reflection;

namespace MarkUnit.Classes
{
    
    public interface IInterfaceRule :  ILogicalLink<IInterfaceTestCondition>, ICheckable{} 
   
    internal interface IInternalInterfaceTestCondition : IInterfaceTestCondition,IInternalCheckable{}

    public interface IInterfaceTestCondition  
    {
        IInterfaceRule HasName(Expression<Predicate<string>> nameFilterExpression);
        IInterfaceRule HasNameMatching(string pattern);
        IInterfaceRule ImplementsInterface<TInterface>();
        IInterfaceRule IsDeclaredInAssemblyMatching(string pattern);
        IInterfaceRule IsDeclaredInAssembly(Expression<Predicate<Assembly>> predicateExpression);
        IInterfaceRule Is(Expression<Predicate<Type>> typeExpressionExpression);
    }

    internal interface ICheckableInterfaceCondition : IInterfaceTestCondition, IInternalCheckable{}
}