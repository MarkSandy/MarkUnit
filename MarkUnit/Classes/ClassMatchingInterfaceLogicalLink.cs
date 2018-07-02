namespace MarkUnit.Classes
{
    internal class ClassMatchingInterfaceLogicalLink
        : LogicalLink<IClassMatchingInterfaceCondition>,
            IClassMatchingInterfaceRule
    {
        private readonly IInternalClassMatchingInterfaceCondition _followUp;

        public ClassMatchingInterfaceLogicalLink(IInternalClassMatchingInterfaceCondition followUp) : base(followUp)
        {
            _followUp = followUp;
        }


        public void Check()
        {
            _followUp.Check();
        }
    }
}