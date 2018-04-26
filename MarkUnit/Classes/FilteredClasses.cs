using System.Collections.Generic;

namespace MarkUnit.Classes
{
    internal class FilteredClasses
        : Filter<IClassInfo>,
          IFilteredClasses
    {
        public FilteredClasses(IEnumerable<IClassInfo> classes)
            : base(classes) { }
    }
}
