using System;

namespace MarkUnit
{
    internal interface IAssertionVerifier<T>
    {
        IFilter<T> Items { get; }
        void AppendCondition(Predicate<T> func);
        void Negate();
        void Verify();
    }
}
