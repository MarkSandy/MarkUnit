using System.Collections.Generic;

namespace MarkUnit
{
    public interface ICollector<out T>
    {
        IEnumerable<T> Get();
    }
}
