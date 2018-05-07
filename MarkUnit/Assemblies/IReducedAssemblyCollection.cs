namespace MarkUnit.Assemblies
{
    public interface IReducedAssemblyCollection : IFilterConditionChain<IAssemblyCollection, IAssemblyTestCondition>, IFollowUp<IAssemblyCollection> { }
}
