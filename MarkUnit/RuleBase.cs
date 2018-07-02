using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

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
            PredicateString.Add("and");
            return LogicalLink.And();
        }

        public TLogicalLink HaveName(Expression<Predicate<string>> nameFilterExpression)
        {
            PredicateString.Add($"has name ({nameFilterExpression})");
            var nameFilter = nameFilterExpression.Compile();
            return AppendCondition(c => nameFilter(c.Name));
        }
        
        public TLogicalLink HaveNameMatching(string pattern)
        {
            PredicateString.Add($"has name matching '{pattern}'");
            return AppendCondition(c => c.Name.Matches(pattern));
        }
        
    }
}