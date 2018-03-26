using System;
using System.Linq;

namespace MarkUnit
{
    
    internal class AssertionVerifier<T> : IAssertionVerifier<T> 
    {
        private readonly IFilter<T> _assertions;
        private readonly bool _negateAssertion;
        private readonly ITestResultLogger<T> _testResultLogger;

        public AssertionVerifier(IFilter<T> items, IFilter<T> assertions, bool negateAssertion, ITestResultLogger<T> testResultLogger)
        {
            Items = items;
            _assertions = assertions;
            _negateAssertion = negateAssertion;
            _testResultLogger = testResultLogger;
        }

        public IFilter<T> Items { get; }

        private bool TestsPassed()
        {
            if (_negateAssertion)
            {
                if (_assertions.FilteredItems.Any())
                {
                    return false;
                }
            }
            else if (_assertions.FilteredItems.Count() != Items.FilteredItems.Count())
            {
                return false;
            }

            return true;
        }

        void IAssertionVerifier<T>.Verify()
        {
            _assertions.Materialize();
            if (TestsPassed())
                _testResultLogger.LogTestsPassed();
            else
                _testResultLogger.LogTestsFailed(_assertions.FilteredItems);
        }

        public void Negate()
        {
            _assertions.Negate();
        }

        public void AppendCondition(Predicate<T> func)
        {
            _assertions.AppendCondition(func);
         }
    }
}