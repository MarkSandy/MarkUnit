using System.Collections.Generic;

namespace MarkUnit.Classes
{
    internal class FilteredInterfaces
        : Filter<IInterface>,
          IFilteredInterfaces
    {
        public FilteredInterfaces(IEnumerable<IInterface> interfaces)
            : base(interfaces) { }
    }
}
