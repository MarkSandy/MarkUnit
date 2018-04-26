using System.Collections.Generic;
using System.Reflection;

namespace MarkUnit.Assemblies
{
    public interface IAssemblyReader
    {
        IEnumerable<IAssemblyInfo> AllAssemblies { get; }
        string AssemblyPath { get; set; }
        void Loadall(Assembly mainAssembly);
        IAssemblyInfo LoadAssembly(string fullName);
        IAssemblyInfo LoadAssembly(Assembly assembly);
    }
}
