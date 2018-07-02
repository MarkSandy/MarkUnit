namespace MarkUnit.Classes 
{
    internal class ClassLogicalLink : LogicalLink<IClassTestCondition>, IClassRule 
    {
        private readonly IInternalClassTestCondition _followUp;

        public ClassLogicalLink(IInternalClassTestCondition followUp) : base(followUp)
        {
            _followUp = followUp;
        }

        public void Check()
        {
            _followUp.Check(); 
        }
    }
}