namespace MarkUnit.Classes
{
    internal class ClassUsingClassLogicalLink
        : LogicalLink<IClassUsesClassCondition>,
            IClassUsingClassRule
    {
        private readonly IInternalClassUsesClassCondition _followUp;

        public ClassUsingClassLogicalLink(IInternalClassUsesClassCondition followUp) : base(followUp)
        {
            _followUp = followUp;
        }


        public void Check()
        {
            _followUp.Check();
        }
    }
}