namespace MarkUnit.Classes
{
    internal interface IClassRuleFactory
    {
        ClassRule Create(IFilter<IClassInfo> filter, bool negateAssertion);
    }

    internal class ClassRuleFactory : IClassRuleFactory
    {
        public ClassRule Create(IFilter<IClassInfo> filter, bool negateAssertion)
        {
            var assertions = new Filter<IClassInfo>(filter.FilteredItems);
            var verifier = new AssertionVerifier<IClassInfo>(filter, assertions, negateAssertion, new TestResultLogger());

            return new ClassRule(verifier);
        }
    }
}