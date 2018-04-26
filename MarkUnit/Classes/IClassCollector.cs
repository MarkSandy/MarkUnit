using MarkUnit.Assemblies;

namespace MarkUnit.Classes
{
    internal interface IClassCollector : ICollector<IClassInfo>
    {
        IFilteredAssemblies Assemblies { get; set; }
    }
}
