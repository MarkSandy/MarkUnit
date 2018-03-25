using System;

namespace MarkUnit
{
    internal class FilterConditionBase<TCondition, TAssertion, T>
        : LogicalLink<TCondition>, IFilterConditionChain<TCondition, TAssertion>
    {
        private readonly IFilter<T> _filter;
        private readonly bool _negate;
        protected Func<IFilter<T>, bool, TAssertion> CeateAssertionFunc;

        public FilterConditionBase(TCondition condition, IFilter<T> filter, bool negate) : base(condition)
        {
            _filter = filter;
            _negate = negate;
        }

        public TAssertion Should()
        {
            PredicateString.Add("should");
            _filter.Materialize();
            return CeateAssertionFunc(_filter, _negate);
        }
    }
}