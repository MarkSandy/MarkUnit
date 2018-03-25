namespace MarkUnit
{
    public interface IFilterConditionChain<out TCondition, out TAssertion> 
        : ILogicalLink<TCondition>
    {
        TAssertion Should();
    }
}