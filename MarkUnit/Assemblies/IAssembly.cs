using System.Collections.Generic;
using System.Reflection;
using MarkUnit.Classes;

namespace MarkUnit.Assemblies
{
    public interface IAssembly : INamedComponent
    {
        IAssembly[] ReferencedAssemblies { get; set; }
        Assembly Assembly { get; }
        void AddClass(IClass classType);
        IEnumerable<IClass> Classes { get; }
    }
}