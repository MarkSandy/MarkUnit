namespace MarkUnit.Classes
{
    internal interface IClassRuleFactory
    {
        ClassRule Create(IFilter<IClass> filter, bool negateAssertion);
    }

    internal class ClassRuleFactory : IClassRuleFactory
    {
        public ClassRule Create(IFilter<IClass> filter, bool negateAssertion)
        {
            var assertions = new Filter<IClass>(filter.FilteredItems);
            var verifier = new AssertionVerifier<IClass>(filter, assertions, negateAssertion, new TestResultLogger());

            return new ClassRule(verifier);
        }
    }
}