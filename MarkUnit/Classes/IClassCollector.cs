using MarkUnit.Assemblies;

namespace MarkUnit.Classes
{
    internal interface IClassCollector : ICollector<IClass>
    {
        IFilteredAssemblies Assemblies { get; set; }
    }

    internal interface ITypeCollector : ICollector<IClass>
    {
        IFilteredAssemblies Assemblies { get; set; }
    }
}
