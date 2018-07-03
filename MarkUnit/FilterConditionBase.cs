using System;
using System.Linq;

namespace MarkUnit
{
    internal class FilterConditionBase<TCondition, TAssertion, T>
        : LogicalLink<TCondition>,
          IFilterConditionChain<TCondition, TAssertion>
    {
        protected Func<IFilter<T>, bool, TAssertion> CeateAssertionFunc;
        private readonly IFilter<T> _filter;

        protected FilterConditionBase(TCondition condition, IFilter<T> filter, bool negate)
            : base(condition)
        {
            _filter = filter;
            Negate = negate;
        }

        public bool Negate { get; }

        public TAssertion Should()
        {
            _filter.Materialize();
            if (!_filter.FilteredItems.Any())
                PredicateString.AddWarning($"No matches for {PredicateString.Text}");
            PredicateString.Add("should");
            return CeateAssertionFunc(_filter, Negate);
        }
 
    }
}
