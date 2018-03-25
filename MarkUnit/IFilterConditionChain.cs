﻿namespace MarkUnit
{
    public interface IFilterConditionChain<out TCondition, out TAssertion> 
        : ILogicalLink<TCondition>
    {
        TAssertion Should();
    }

    internal interface IInternalFilterConditionChain<out TCondition, out TAssertion> : IFilterConditionChain<TCondition, TAssertion>
    {

    }
}