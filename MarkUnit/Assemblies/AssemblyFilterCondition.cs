namespace MarkUnit.Assemblies
{
    internal class AssemblyFilterCondition
        : FilterConditionBase<IAssemblyCollection, IAssemblyTestCondition, IAssemblyInfo>,
          IAssemblyFilterCondition,
          IReducedAssemblyCollection
    {
        public AssemblyFilterCondition(IAssemblyCollection condition, IFilter<IAssemblyInfo> filter, bool negate)
            : base(condition, filter, negate)
        {
            CeateAssertionFunc = (f, b) => new AssemblyRule(f, b);
        }
    }
}
