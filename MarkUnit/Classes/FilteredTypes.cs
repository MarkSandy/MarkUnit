using System.Collections.Generic;

namespace MarkUnit.Classes
{
    internal class FilteredTypes
        : Filter<IType>,
            IFilteredTypes
    {
        public FilteredTypes(IEnumerable<IType> classes)
            : base(classes) { }
    }
}