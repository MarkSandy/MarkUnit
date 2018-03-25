using System.Collections.Generic;

namespace MarkUnit.Assemblies
{
    public interface IAssemblyCollector : ICollector<IAssembly>
    {
        string Pattern { get; set; }
        IEnumerable<IAssembly> SolutionAssemblies { get; }
    }
}
