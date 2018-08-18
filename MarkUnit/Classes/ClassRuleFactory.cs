namespace MarkUnit.Classes
{
    internal class ClassRuleFactory : IClassRuleFactory
    {
        public ClassRule Create(IFilter<IClass> filter, bool negateAssertion)
        {
            var assertions = new Filter<IClass>(filter.FilteredItems);
            var verifier = Instances.CreateAssertionVerifier(filter, assertions, negateAssertion);
            return new ClassRule(verifier);
        }
    }
}
