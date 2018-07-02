using System.Collections.Generic;
using System.Linq;
using MarkUnit.Assemblies;

namespace MarkUnit.Classes
{
    internal class InterfaceFromTypeCollector : IInterfaceCollector
    {
        private readonly IEnumerable<IInterface> _interfaces;

        public InterfaceFromTypeCollector(IFilter<IType> filter)
        {
            _interfaces = filter.FilteredItems.Select(t => new MarkUnitInterface(new MarkUnitClass(t.Assembly, t.ClassType)));
        }

        public IEnumerable<IInterface> Get()
        {
            return _interfaces;
        }

        public IFilteredAssemblies Assemblies { get; set; }
    }
}