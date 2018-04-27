using System.Collections.Generic;
using System.Reflection;
using MarkUnit.Classes;

namespace MarkUnit.Assemblies
{
    public interface IAssemblyInfo : INamedComponent
    {
        IAssembly Assembly { get; }
        IEnumerable<IClassInfo> Classes { get; }
        IAssemblyInfo[] ReferencedAssemblies { get; set; }
        void AddClass(IClassInfo classType);
    }
}
