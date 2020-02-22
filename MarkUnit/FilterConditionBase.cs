using System;
using System.Linq;

namespace MarkUnit
{
    internal class FilterConditionBase<TCondition, TAssertion, T>
        : IFilterConditio<TCondition, TAssertion>
    {
        protected Func<IFilter<T>, bool, TAssertion> CeateAssertionFunc;
        private readonly IFilter<T> _filter;

        protected FilterConditionBase(TCondition condition, IFilter<T> filter, bool negate)
        {
            _filter = filter;
            Negate = negate;
            Condition = condition;
        }

        public bool Negate { get; }
        private TCondition Condition { get; }

        public TAssertion Should()
        {
            _filter.Materialize();
            if (!_filter.FilteredItems.Any())
                PredicateString.AddWarning($"No matches for {PredicateString.Text}");
            PredicateString.Add("should");
            return CeateAssertionFunc(_filter, Negate);
        }

        public TCondition And()
        {
            PredicateString.Add("and");
            return Condition;
        }
    }
}
