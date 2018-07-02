namespace MarkUnit.Classes
{
    internal class InterfaceRule
        : FunctionalComponentRule<IInterface, IInterfaceTestCondition, IInterfaceRule>,
          IInternalInterfaceTestCondition
    {
        public InterfaceRule(IFilter<IInterface> items, bool negateAssertion)
            : base(items, negateAssertion)
        {
            LogicalLink = new InterfaceLogicalLink(this);
        }

        public InterfaceRule(IAssertionVerifier<IInterface> verifier) : base(verifier)
        {
            LogicalLink = new InterfaceLogicalLink(this);
        }
    }
}
