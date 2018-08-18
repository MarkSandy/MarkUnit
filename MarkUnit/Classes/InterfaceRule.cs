namespace MarkUnit.Classes
{
    internal class InterfaceRule
        : FunctionalComponentRule<IInterface, IInterfaceTestCondition, IInterfaceRule>,
          IInternalInterfaceTestCondition,
          IInterfaceRule
    {
        public InterfaceRule(IFilter<IInterface> items, bool negateAssertion)
            : base(items, negateAssertion) { }

        public InterfaceRule(IAssertionVerifier<IInterface> verifier)
            : base(verifier) { }

        protected override IInterfaceRule LogicalLink => this;
    }
}
