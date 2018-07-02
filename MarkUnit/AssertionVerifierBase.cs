using System;
using System.Linq;

namespace MarkUnit
{
    internal abstract class AssertionVerifierBase<T> : IAssertionVerifier<T> where T : INamedComponent
    {
        protected IFilter<T> Assertions { get; }
        protected bool NegateAssertion { get; }
        protected ITestResultLogger TestResultLogger { get; }

        protected AssertionVerifierBase(IFilter<T> items, IFilter<T> assertions, bool negateAssertion, ITestResultLogger testResultLogger)
        {
            Items = items;
            Assertions = assertions;
            NegateAssertion = negateAssertion;
            TestResultLogger = testResultLogger;
        }

        public IFilter<T> Items { get; }

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

        void IAssertionVerifier<T>.Verify()
        {
            InnerVerify();
        }

        public void InnerVerify()
        {
            Assertions.Materialize();
            HandleResult();
        }

        protected void HandleResult()
        {
            if (TestsPassed())
            {
                TestResultLogger.LogTestsPassed();
            }
            else
            {
                var failedItems = NegateAssertion ? Assertions.FilteredItems : Items.FilteredItems.Where(x=> Assertions.FilteredItems.All(a => a.Name != x.Name));
                TestResultLogger.LogTestsFailed(failedItems.Cast<INamedComponent>());
            }
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

        protected abstract void HandleAppendedCondition();
    }
}