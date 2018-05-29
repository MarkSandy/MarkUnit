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

    internal class TypeLogicalLink : LogicalLink<ITypeTestCondition>, ITypeRule 
    {
        private readonly IInternalTypeTestCondition _followUp;

        public TypeLogicalLink(IInternalTypeTestCondition followUp) : base(followUp)
        {
            _followUp = followUp;
        }

        public void Check()
        {
            _followUp.Check(); 
        }
    }
}