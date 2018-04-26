using System;
using System.Linq.Expressions;

namespace MarkUnit.Assemblies
{
    public interface IAssemblyTestCondition
    {
        IAssemblyTestCondition Not();
        IAssemblyRule ReferenceAssembliesMatching(string pattern);
        IAssemblyRule ReferenceAssembliesMatching(Expression<Predicate<IAssemblyInfo>> assemblyFilterExpression);
        IAssemblyRule ReferenceAssembly(string name);
        IAssemblyRule HaveName(Expression<Predicate<string>> nameFilterExpression);
    }
}
