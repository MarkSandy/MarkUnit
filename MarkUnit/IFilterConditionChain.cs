namespace MarkUnit
{
    public interface IFilterConditionChain<out TCondition, out TAssertion>
        : IRule<TCondition>,
          IShould<TAssertion>
    {
        bool Negate { get; }
    }

    public interface IShould<out TAssertion>
    {
        TAssertion Should();
    }
}
