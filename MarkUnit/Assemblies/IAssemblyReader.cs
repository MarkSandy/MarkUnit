using System.Collections.Generic;
using System.Reflection;

namespace MarkUnit.Assemblies
{
    public interface IAssemblyReader
    {
        IAssembly LoadAssembly(string fullName);
        IAssembly LoadAssembly(Assembly assembly);
        void Loadall(Assembly mainAssembly);
        IEnumerable<IAssembly> AllAssemblies { get; }
        string AssemblyPath { get; set; }
    }
}