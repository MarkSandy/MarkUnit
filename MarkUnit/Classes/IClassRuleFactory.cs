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
            var verifier = Instances.CreateAssertionVerifier(filter,assertions,negateAssertion);
            return new ClassRule(verifier);
        }
    }

    internal interface ITypeRuleFactory
    {
        TypeRule Create(IFilter<IType> filter, bool negateAssertion);
    }

    internal class TypeRuleFactory : ITypeRuleFactory
    {
        public TypeRule Create(IFilter<IType> filter, bool negateAssertion)
        {
            var assertions = new Filter<IType>(filter.FilteredItems);
            var verifier = Instances.CreateAssertionVerifier(filter,assertions,negateAssertion);
            return new TypeRule(verifier);
        }
    }
}