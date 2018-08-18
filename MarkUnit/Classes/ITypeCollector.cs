using MarkUnit.Assemblies;

namespace MarkUnit.Classes
{
    internal interface ITypeCollector : ICollector<IType>
    {
        IFilteredAssemblies Assemblies { get; set; }
    }
}
