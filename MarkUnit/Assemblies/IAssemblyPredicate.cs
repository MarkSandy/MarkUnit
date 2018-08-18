using MarkUnit.Classes;

namespace MarkUnit.Assemblies
{
    public interface IAssemblyPredicate
        : IPredicate<IAssemblyCollection>,
          IExceptions<IAssemblyCollection> { }
}
