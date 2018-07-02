using MarkUnit.Classes;

namespace MarkUnit
{
    public interface IFilterConditionChain<out TCondition, out TAssertion> 
        : IRule<TCondition> 
    {
        bool Negate { get; }
         TAssertion Should();
    }
}