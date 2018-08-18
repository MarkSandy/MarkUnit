using System.Collections.Generic;
using MarkUnit.Assemblies;

namespace MarkUnit.Classes
{
    internal interface ITypeReader<out T>
        where T : IType
    {
        IEnumerable<T> LoadFromAssemblies(IFilteredAssemblies assemblies);
    }
}
