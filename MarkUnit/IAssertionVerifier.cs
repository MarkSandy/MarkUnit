using System;

namespace MarkUnit
{
    internal interface IAssertionVerifier<T> where T : INamedComponent
    {
        void Negate();
        void AppendCondition(Predicate<T> func);
    }
}