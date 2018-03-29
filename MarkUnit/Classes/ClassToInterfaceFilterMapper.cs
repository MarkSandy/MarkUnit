using System;
using System.Collections.Generic;
using System.Linq;

namespace MarkUnit.Classes
{
    internal class ClassToInterfaceFilterMapper : IFilter<IInterface>
    {
        private readonly IFilter<IClass> _filter;

        public IEnumerable<IInterface> FilteredItems => _filter.FilteredItems.Select(ConvertToIInterface);

        private IInterface ConvertToIInterface(IClass @class)
        {
            return new MarkUnitInterface(@class);
        }

        public ClassToInterfaceFilterMapper(IFilter<IClass> filter)
        {
            _filter = filter;
        }
        
        public void AppendCondition(Predicate<IInterface> func)
        {
            _filter.AppendCondition(c=>func((IInterface)c));
        }

        public void Materialize()
        {
            _filter.Materialize();
        }

        public void Negate()
        {
            _filter.Negate();
        }
    }
}
