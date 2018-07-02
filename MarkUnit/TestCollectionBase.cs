using System;
using System.Linq.Expressions;
using MarkUnit.Classes;

namespace MarkUnit
{
    internal class TestCollectionBase<T, TCondition, TAssertion, TPostCondition>
        : IRule<TCondition>
        where TPostCondition :  IFilterConditionChain<TCondition, TAssertion>
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

        public TCondition Not()
        {
            PredicateString.Add("not");
            return SilentNot();
        }

        internal TCondition SilentNot()
        {
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
       
    }
}
