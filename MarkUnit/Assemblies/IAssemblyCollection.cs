using MarkUnit.Classes;

namespace MarkUnit.Assemblies
{
    public interface IAssemblyCollection
        : ICondition<IAssemblyCollection>,
          INamePredicate<IReducedAssemblyCollection> { }
}
