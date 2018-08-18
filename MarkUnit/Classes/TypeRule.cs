namespace MarkUnit.Classes
{
    internal class TypeRule
        : TypeComponentRule<IType, ITypeTestCondition, ITypeRule>,
          IInternalTypeTestCondition,
          ITypeRule
    {
        public TypeRule(IAssertionVerifier<IType> verifier)
            : base(verifier) { }

        public TypeRule(IFilter<IType> items, bool negateAssertion)
            : base(items, negateAssertion) { }

        protected override ITypeRule LogicalLink => this;
    }
}
