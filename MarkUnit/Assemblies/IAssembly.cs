using System.Collections.Generic;
using System.Reflection;
using MarkUnit.Classes;

namespace MarkUnit.Assemblies
{
    public interface IAssembly : INamedComponent
    {
        Assembly Assembly { get; }
        IEnumerable<IClass> Classes { get; }
        IAssembly[] ReferencedAssemblies { get; set; }
        void AddClass(IClass classType);
    }
}
