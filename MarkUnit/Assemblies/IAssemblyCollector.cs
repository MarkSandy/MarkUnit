using System.Collections.Generic;

namespace MarkUnit.Assemblies
{
    public interface IAssemblyCollector : ICollector<IAssembly>
    {
        IEnumerable<IAssembly> SolutionAssemblies { get; }
        string Pattern { get; set; }
    }
}