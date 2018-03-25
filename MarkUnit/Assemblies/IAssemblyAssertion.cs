using System;

namespace MarkUnit.Assemblies
{
    
    public interface IAssemblyTestCondition   
    {
        IAssemblyTestCondition Not();
        IAssemblyRule ReferenceAssembly(string name);
        IAssemblyRule ReferenceAssembliesMatching(string pattern);
        IAssemblyRule ReferenceAssembliesMatching(Predicate<IAssembly> func);
    }

    public interface IAssemblyRule :  ILogicalLink<IAssemblyTestCondition>, ICheckable{} 
    // IAssemblyTestCondition And() =>  + void Check();

   internal interface IInternalAssemblyTestCondition : IAssemblyTestCondition,IInternalCheckable{}
}