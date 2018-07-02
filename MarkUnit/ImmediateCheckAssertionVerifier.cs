namespace MarkUnit
{
    internal class ImmediateCheckAssertionVerifier<T> : AssertionVerifierBase<T> where T : INamedComponent
    {
        public ImmediateCheckAssertionVerifier(IFilter<T> items, IFilter<T> assertions, bool negateAssertion, ITestResultLogger testResultLogger) : base(items, assertions, negateAssertion, testResultLogger)
        {
        }

        protected override void HandleAppendedCondition()
        {
            InnerVerify();
        }
    }
}