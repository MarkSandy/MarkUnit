using System.Collections.Generic;

namespace MarkUnit.Assemblies
{
    public interface IAssemblyCollector : ICollector<IAssemblyInfo>
    {
        string Pattern { get; set; }
        IEnumerable<IAssemblyInfo> SolutionAssemblies { get; }
    }
}
