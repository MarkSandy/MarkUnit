using System.Collections.Generic;
using MarkUnit.Assemblies;

namespace MarkUnit.Classes
{
    internal interface IClassReader
    {
        IEnumerable<IClass> LoadFromAssemblies(IFilteredAssemblies assemblies);
    }
}
