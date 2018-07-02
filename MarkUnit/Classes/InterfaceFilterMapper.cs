using System;
using System.Collections.Generic;

namespace MarkUnit.Classes
{
    internal class InterfaceFilterMapper : IFilter<IInterface>
    {        
        private readonly IFilter<IInterface> _filter;

        public IEnumerable<IInterface> FilteredItems => _filter.FilteredItems;
            
        public InterfaceFilterMapper(IFilter<IInterface> filter)
        {
            _filter = filter;
        }
        
        public void AppendCondition(Predicate<IInterface> func)
        {
            _filter.AppendCondition(func);
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