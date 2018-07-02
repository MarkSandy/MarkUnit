namespace MarkUnit.Classes
{
    internal class TypeRule : TypeComponentRule<IType, ITypeTestCondition,ITypeRule>, IInternalTypeTestCondition
    {

        public TypeRule(IAssertionVerifier<IType> verifier) : base(verifier)
        {
            LogicalLink = new TypeLogicalLink(this);
        }

        public TypeRule(IFilter<IType> items, bool negateAssertion) : base(items, negateAssertion)
        {
            LogicalLink = new TypeLogicalLink(this);
        }


    }
}