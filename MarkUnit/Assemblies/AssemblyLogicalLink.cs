namespace MarkUnit.Assemblies
{
    internal class AssemblyLogicalLink : LogicalLink<IAssemblyTestCondition>, IAssemblyRule 
    {
        private readonly IInternalAssemblyTestCondition _followUp;

        public AssemblyLogicalLink(IInternalAssemblyTestCondition followUp) : base(followUp)
        {
            _followUp = followUp;
        }

        public void Check()
        {
            _followUp.Check(); 
        }
    }
}