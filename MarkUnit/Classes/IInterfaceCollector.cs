using MarkUnit.Assemblies;

namespace MarkUnit.Classes
{
    internal interface IInterfaceCollector : ICollector<IInterface>
    {
        IFilteredAssemblies Assemblies { get; set; }
    }
}