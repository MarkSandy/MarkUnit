namespace MarkUnit
{
    internal interface IInternalFilterConditionChain<out TCondition, out TAssertion> : IFilterConditio<TCondition, TAssertion>
    {
        TCondition FollowUp { get; }
    }
}
