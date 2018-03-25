using System;

namespace MarkUnit
{
    internal interface IAssertionVerifier<T> 
    {
        void Negate();
        void AppendCondition(Predicate<T> func);
    }
}