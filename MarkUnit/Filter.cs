using System;
using System.Collections.Generic;
using System.Linq;

namespace MarkUnit
{
    internal class Filter<T> : IFilter<T>
    {
        public Filter(IEnumerable<T> assemblies)
        {
            FilteredItems = assemblies;
        }

        private bool Not { get; set; }

        public IEnumerable<T> FilteredItems { get; private set; }

        public void Materialize()
        {
            var x = FilteredItems;

            FilteredItems = FilteredItems.ToArray();
        }

        public void AppendCondition(Predicate<T> func)
        {
            FilteredItems = Not
                ? FilteredItems.Where(a => !func(a))
                : FilteredItems.Where(a => func(a));
            Not = false;
        }

        public void Negate()
        {
            Not = !Not;
        }
    }
}
