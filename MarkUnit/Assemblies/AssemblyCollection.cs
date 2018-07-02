namespace MarkUnit.Assemblies
{
    internal class AssemblyCollection
        : TestCollectionBase<IAssembly, IAssemblyCollection, IAssemblyTestCondition, IReducedAssemblyCollection>,
          IAssemblyCollection
    {
        public AssemblyCollection(FilteredAssemblies assemblies, bool negate)
            : base(assemblies)
        {
            FollowUp = this;
            FilterCondition = new AssemblyFilterCondition(this, assemblies, negate);
        }
    }
}
