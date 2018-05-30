using System;
using System.Linq;

namespace MarkUnit
{

    internal class AssertionVerifier<T> : IAssertionVerifier<T> where T : INamedComponent
    {
        private readonly IFilter<T> _assertions;
        private readonly bool _negateAssertion;
        private readonly ITestResultLogger _testResultLogger;

        public AssertionVerifier(IFilter<T> items, IFilter<T> assertions, bool negateAssertion, ITestResultLogger testResultLogger)
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
            {
                var failedItems = _negateAssertion ? _assertions.FilteredItems : Items.FilteredItems.Except(_assertions.FilteredItems);
                _testResultLogger.LogTestsFailed(failedItems.Cast<INamedComponent>());
            }
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

    internal class ImmediateCheckAssertionVerifier<T> : IAssertionVerifier<T> where T : INamedComponent
    {
        private readonly IFilter<T> _assertions;
        private readonly bool _negateAssertion;
        private readonly ITestResultLogger _testResultLogger;

        public ImmediateCheckAssertionVerifier(IFilter<T> items, IFilter<T> assertions, bool negateAssertion, ITestResultLogger testResultLogger)
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
            if (TestsPassed()==false)
            {
                var failedItems = _negateAssertion ? _assertions.FilteredItems : Items.FilteredItems.Except(_assertions.FilteredItems);
                _testResultLogger.LogTestsFailed(failedItems.Cast<INamedComponent>());
            }
        }

        public void Negate()
        {
            _assertions.Negate();
        }

        public void AppendCondition(Predicate<T> func)
        {
            _assertions.AppendCondition(func);
        }
    }}