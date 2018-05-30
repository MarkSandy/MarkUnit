using MarkUnit.Assemblies;

namespace MarkUnit
{
    public interface INamedComponent
    {
        string Name { get; }
    }

    public interface IAssemblyComponent
    {
        IAssembly Assembly { get; }
    }
}
