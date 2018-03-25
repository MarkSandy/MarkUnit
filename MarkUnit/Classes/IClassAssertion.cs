using System;
using System.Linq.Expressions;

namespace MarkUnit.Classes
{
    public interface IClassTestCondition 
    {
        IClassTestCondition Not();
        IClassRule HaveNameMatching(string pattern);
        IClassRule HaveName(Expression<Predicate<string>> nameFunc);
        
        IClassRule ReferenceNamespacesMatching(string pattern);
        IClassRule ImplementInterfaceMatching(string pattern);
        IClassRule ImplementInterface<T>();

        IInterfacePredicate ImplementInterface();
        IClassRule UsesClassMatching(string regExOnClassName, string regExreplace);
    }
    public interface IClassRule :  ILogicalLink<IClassTestCondition>, ICheckable{} 
   
    internal interface IInternalClassTestCondition : IClassTestCondition,IInternalCheckable{}
 }