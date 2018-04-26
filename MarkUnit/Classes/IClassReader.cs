using System.Collections.Generic;
using MarkUnit.Assemblies;

namespace MarkUnit.Classes
{
    internal interface IClassReader
    {
        IEnumerable<IClassInfo> LoadFromAssemblies(IFilteredAssemblies assemblies);
    }
}
