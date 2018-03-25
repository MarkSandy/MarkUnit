namespace MarkUnit.Classes
{
    internal class InterfaceLogicalLink
        : LogicalLink<IInterfaceTestCondition>,
          IInterfaceRule
    {
        private readonly IInternalInterfaceTestCondition _followUp;

        public InterfaceLogicalLink(IInternalInterfaceTestCondition followUp)
            : base(followUp)
        {
            _followUp = followUp;
        }

        public void Check()
        {
            _followUp.Check();
        }
    }
}
