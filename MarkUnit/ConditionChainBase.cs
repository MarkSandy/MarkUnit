using System;

namespace MarkUnit
{
    internal class TestCollectionBase<T, TCondition, TAssertion, TPostCondition> 
        : ILogicalLink<TCondition> 
        where TPostCondition :IFilterConditionChain<TCondition,TAssertion>
    {
        protected IFilter<T> Filter;

        public TestCollectionBase(IFilter<T> items)
        {
            Filter = items;
        }

        protected TPostCondition FilterCondition { get; set; }

        protected  TPostCondition  AppendCondition(Predicate<T> predicate)
        {
            Filter.AppendCondition(predicate);
            return FilterCondition;
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

        public TCondition And()
        {
            return FilterCondition.And();
        }
    }
}