using System;

namespace MarkUnit.Assemblies
{
    public interface IAssemblyTestCondition
    {
        IAssemblyTestCondition Not();
        IAssemblyRule ReferenceAssembliesMatching(string pattern);
        IAssemblyRule ReferenceAssembliesMatching(Predicate<IAssembly> func);
        IAssemblyRule ReferenceAssembly(string name);
    }

    // IAssemblyTestCondition And() =>  + void Check();
}
