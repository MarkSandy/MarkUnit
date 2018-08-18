using System;
using System.Linq;

namespace MarkUnit
{
    internal abstract class AssertionVerifierBase<T> : IAssertionVerifier<T>
        where T : INamedComponent
    {
        protected AssertionVerifierBase(IFilter<T> items, IFilter<T> assertions, bool negateAssertion, ITestResultLogger testResultLogger)
        {
            Items = items;
            Assertions = assertions;
            NegateAssertion = negateAssertion;
            TestResultLogger = testResultLogger;
        }

        protected IFilter<T> Assertions { get; }
        protected bool NegateAssertion { get; }
        protected ITestResultLogger TestResultLogger { get; }

        public IFilter<T> Items { get; }

        void IAssertionVerifier<T>.Verify()
        {
            InnerVerify();
        }

        public void Negate()
        {
            Assertions.Negate();
        }

        public void AppendCondition(Predicate<T> func)
        {
            Assertions.AppendCondition(func);
            HandleAppendedCondition();
        }

        public void InnerVerify()
        {
            Assertions.Materialize();
            HandleResult();
        }

        protected abstract void HandleAppendedCondition();

        protected void HandleResult()
        {
            if (TestsPassed())
            {
                TestResultLogger.LogTestsPassed();
            }
            else
            {
                var failedItems = NegateAssertion
                    ? Assertions.FilteredItems
                    : Items.FilteredItems.Where(x => Assertions.FilteredItems.All(a => a.Name != x.Name));
                TestResultLogger.LogTestsFailed(failedItems.Cast<INamedComponent>());
            }
        }

        protected bool TestsPassed()
        {
            if (NegateAssertion)
            {
                if (Assertions.FilteredItems.Any())
                {
                    return false;
                }
            }
            else if (Assertions.FilteredItems.Count() != Items.FilteredItems.Count())
            {
                return false;
            }

            return true;
        }
    }
}
