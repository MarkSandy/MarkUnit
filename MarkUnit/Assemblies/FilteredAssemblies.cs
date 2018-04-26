using System.Collections.Generic;

namespace MarkUnit.Assemblies
{
    internal class FilteredAssemblies
        : Filter<IAssemblyInfo>,
          IFilteredAssemblies
    {
        public FilteredAssemblies(IEnumerable<IAssemblyInfo> assemblies)
            : base(assemblies) { }
    }
}
