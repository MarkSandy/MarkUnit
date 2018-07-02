namespace MarkUnit.Classes
{
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