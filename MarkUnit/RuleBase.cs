using System;
using System.Linq.Expressions;

namespace MarkUnit
{
    internal abstract class RuleBase<T, TAssertion, TLogicalLink>
        : IRule<TAssertion>,
          ICheckable
        where T : INamedComponent
        where TLogicalLink : IRule<TAssertion>
    {
        protected RuleBase(IFilter<T> items, bool negateAssertion)
        {
            var assertions = new Filter<T>(items.FilteredItems);
            Verifier = Instances.CreateAssertionVerifier(items, assertions, negateAssertion);
        }

        protected RuleBase(IAssertionVerifier<T> verifier)
        {
            Verifier = verifier;
        }

        protected abstract TLogicalLink LogicalLink { get; }
        internal IAssertionVerifier<T> Verifier { get; }

        public void Check()
        {
            Verifier.Verify();
        }

        public TAssertion And()
        {
            PredicateString.Add("and");
            return LogicalLink.And();
        }

        public TLogicalLink HaveName(Expression<Predicate<string>> nameFilterExpression)
        {
            PredicateString.Add($"have name ({nameFilterExpression})");
            var nameFilter = nameFilterExpression.Compile();
            return AppendCondition(c => nameFilter(c.Name));
        }

        public TLogicalLink HaveNameMatching(string pattern)
        {
            PredicateString.Add($"have name matching '{pattern}'");
            return AppendCondition(c => c.Name.Matches(pattern));
        }

        public TAssertion Not()
        {
            PredicateString.Add("not");
            Verifier.Negate();
            return LogicalLink.And();
        }

        protected TLogicalLink AppendCondition(Predicate<T> predicate)
        {
            Verifier.AppendCondition(predicate);
            return LogicalLink;
        }
    }
}
