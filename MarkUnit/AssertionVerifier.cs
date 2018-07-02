using MarkUnit;

namespace MarkUnit
{
    internal class AssertionVerifier<T> : AssertionVerifierBase<T> where T : INamedComponent
    {
        public AssertionVerifier(IFilter<T> items, IFilter<T> assertions, bool negateAssertion, ITestResultLogger testResultLogger) : base(items, assertions, negateAssertion, testResultLogger)
        {
        }
        protected override void HandleAppendedCondition()
        {
        }
    }
}