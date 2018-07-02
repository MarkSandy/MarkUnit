namespace MarkUnit.Classes
{
    internal class InterfaceRuleFactory : IInterfaceRuleFactory
    {
        public InterfaceRule Create(IFilter<IInterface> filter, bool negateAssertion)
        {
            var assertions = new Filter<IInterface>(filter.FilteredItems);
            var verifier = Instances.CreateAssertionVerifier(filter,assertions,negateAssertion);
            return new InterfaceRule(verifier);
        }
    }
}