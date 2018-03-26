namespace MarkUnit
{
    public interface IFilterConditionChain<out TCondition, out TAssertion> 
        : IRule<TCondition>
    {
        TAssertion Should();
    }
}