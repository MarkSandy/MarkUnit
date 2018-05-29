using System.Collections.Generic;

namespace MarkUnit.Classes
{
    internal class FilteredClasses
        : Filter<IClass>,
          IFilteredClasses
    {
        public FilteredClasses(IEnumerable<IClass> classes)
            : base(classes) { }
    }

    internal class FilteredTypes
        : Filter<IClass>,
            IFilteredTypes
    {
        public FilteredTypes(IEnumerable<IClass> classes)
            : base(classes) { }
    }
}
