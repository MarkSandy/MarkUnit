using System.Collections.Generic;
using System.Reflection;

namespace MarkUnit.Assemblies
{
    public interface IAssemblyReader
    {
        IEnumerable<IAssembly> AllAssemblies { get; }
        string AssemblyPath { get; set; }
        void Loadall(Assembly mainAssembly);
        IAssembly LoadAssembly(string fullName);
        IAssembly LoadAssembly(Assembly assembly);
    }
}
