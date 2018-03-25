namespace MarkUnit.Assemblies
{
    public interface IAssemblyRule
        : ILogicalLink<IAssemblyTestCondition>,
          ICheckable { }
}
