using System;

namespace MarkUnit
{
    internal class RuleBase<T, TAssertion, TRule>
        : ILogicalLink<TAssertion>, ICheckable
        where T : INamedComponent
        where TRule : ILogicalLink<TAssertion>
    {
        protected AssertionVerifier<T> Verifier;

        public RuleBase(IFilter<T> items, bool negateAssertion)
        {
            var assertions = new Filter<T>(items.FilteredItems);
            Verifier = new AssertionVerifier<T>(items, assertions, negateAssertion, new TestResultLogger<T>());
        }

        protected TRule LogicalLink { get; set; }

        protected TRule AppendCondition(Predicate<T> predicate)
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