using System;

namespace MarkUnit
{
    internal class RuleBase<T, TAssertion, TLogicalLink>
        : IRule<TAssertion>, ICheckable
        where T : INamedComponent
        where TLogicalLink : IRule<TAssertion>
    {
        internal IAssertionVerifier<T> Verifier { get; }

        public RuleBase(IFilter<T> items, bool negateAssertion)
        {
            var assertions = new Filter<T>(items.FilteredItems);
            Verifier = Instances.CreateAssertionVerifier(items,assertions,negateAssertion);
        }

        public RuleBase(IAssertionVerifier<T> verifier)
        {
            Verifier = verifier;
        }

        protected TLogicalLink LogicalLink { get; set; }

        protected TLogicalLink AppendCondition(Predicate<T> predicate)
        {
            Verifier.AppendCondition(predicate);
            return LogicalLink;
        }

        public TAssertion Not()
        {
            PredicateString.Add("not");
            Verifier.Negate();
            return LogicalLink.And();
        }

        public void Check()
        {
            Verifier.Verify();
        }

        public TAssertion And()
        {
            return LogicalLink.And();
        }
    }
}