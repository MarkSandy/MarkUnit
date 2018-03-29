using System;
using MarkUnit.Classes;

namespace MarkUnit
{
    internal class TestCollectionBase<T, TCondition, TAssertion, TPostCondition>
        : IRule<TCondition>
        where TPostCondition : IFilterConditionChain<TCondition, TAssertion>
    {
        protected IFilter<T> Filter;

        public TestCollectionBase(IFilter<T> items)
        {
            Filter = items;
        }

        protected TPostCondition FilterCondition { get; set; }

        public TCondition And()
        {
            return FilterCondition.And();
        }

        public TCondition Not()
        {
            Filter.Negate();
            return FilterCondition.And();
        }

        public TAssertion Should()
        {
            return FilterCondition.Should();
        }

        protected TPostCondition AppendCondition(Predicate<T> predicate)
        {
            Filter.AppendCondition(predicate);
            return FilterCondition;
        }
    }
}
