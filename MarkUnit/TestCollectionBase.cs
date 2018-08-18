using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MarkUnit
{
    internal class TestCollectionBase<T, TCondition, TAssertion, TPostCondition>
        : IRule<TCondition>
        where TPostCondition : IFilterConditionChain<TCondition, TAssertion>
        where T : INamedComponent
    {
        protected IFilter<T> Filter;
        protected TCondition FollowUp;

        public TestCollectionBase(IFilter<T> items)
        {
            Filter = items;
        }

        protected TPostCondition FilterCondition { get; set; }

        public TCondition And()
        {
            return FilterCondition.And();
        }

        public TPostCondition HasName(Expression<Predicate<string>> nameFilterExpression)
        {
            var nameFilter = nameFilterExpression.Compile();
            PredicateString.Add($"has name matching '{nameFilterExpression}'");
            return AppendCondition(c => nameFilter(c.Name));
        }

        public TPostCondition HasNameMatching(string pattern)
        {
            PredicateString.Add($"has name matching '{pattern}'");
            return AppendCondition(c => c.Name.Matches(pattern));
        }

        public TPostCondition HasNameMatchingAny(params string[] patterns)
        {
            PredicateString.Add($"has name matching any of {PatternDescription(patterns)}");
            return SilentHasNameMatchingAny(patterns);
        }

        public TCondition Not()
        {
            PredicateString.Add("not");
            Filter.Negate();
            return FollowUp;
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

        internal TCondition AddIgnoreList(string[] patterns, bool not)
        {
            if (patterns.Any())
            {
                PredicateString.Add($"except {PatternDescription(patterns)}");
                PredicateString.AddWarning("Exceptions: " + PatternDescription(patterns));
                Filter.Negate();
                SilentHasNameMatchingAny(patterns);
            }

            if (not) Filter.Negate();
            return FollowUp;
        }

        private static string PatternDescription(string[] patterns)
        {
            return string.Join(",", patterns.Select(s => $"'{s}'"));
        }

        private TPostCondition SilentHasNameMatchingAny(IEnumerable<string> patterns)
        {
            return AppendCondition(c => patterns.Any(p => c.Name.Matches(p)));
        }
    }
}
