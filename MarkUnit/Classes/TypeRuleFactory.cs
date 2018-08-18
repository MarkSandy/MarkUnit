namespace MarkUnit.Classes
{
    internal class TypeRuleFactory : ITypeRuleFactory
    {
        public TypeRule Create(IFilter<IType> filter, bool negateAssertion)
        {
            var assertions = new Filter<IType>(filter.FilteredItems);
            var verifier = Instances.CreateAssertionVerifier(filter, assertions, negateAssertion);
            return new TypeRule(verifier);
        }
    }
}
