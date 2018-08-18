using System.Collections.Generic;
using System.Linq;
using MarkUnit.Assemblies;

namespace MarkUnit.Classes
{
    internal class ClassFromTypeCollector : IClassCollector
    {
        private readonly IEnumerable<IClass> _classes;

        public ClassFromTypeCollector(IFilter<IType> filter)
        {
            _classes = filter.FilteredItems.Select(t => new MarkUnitClass(t.Assembly, t.ClassType));
        }

        public IEnumerable<IClass> Get()
        {
            return _classes;
        }

        public IFilteredAssemblies Assemblies { get; set; }
    }
}
