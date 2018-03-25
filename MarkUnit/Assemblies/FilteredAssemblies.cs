using System.Collections.Generic;

namespace MarkUnit.Assemblies
{
    internal class FilteredAssemblies : Filter<IAssembly>,IFilteredAssemblies
    {
        public FilteredAssemblies(IEnumerable<IAssembly> assemblies)
            : base(assemblies) { }
    }
}