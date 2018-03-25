using System;
using System.Collections.Generic;

namespace MarkUnit
{
    internal interface IFilter<T>
    {
        IEnumerable<T> FilteredItems { get; }
        void AppendCondition(Predicate<T> func);
        void Materialize();
        void Negate();
    }

    public interface INamedComponent
    {
        string Name { get; }
    }
}
