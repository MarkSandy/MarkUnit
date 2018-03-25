namespace MarkUnit.Assemblies
{
    internal class AssemblyFilterCondition
        : FilterConditionBase<IAssemblyCollection, IAssemblyTestCondition, IAssembly>,
          IAssemblyFilterCondition,
          IReducedAssemblyCollection
    {
        public AssemblyFilterCondition(IAssemblyCollection condition, IFilter<IAssembly> filter, bool negate)
            : base(condition, filter, negate)
        {
            CeateAssertionFunc = (f, b) => new AssemblyRule(f, b);
        }
    }
}
