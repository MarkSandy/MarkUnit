using System;
using System.Reflection;

namespace MarkUnit.Assemblies
{
    public interface IAssembly
    {
        Assembly Assembly { get; }
        string FullName { get; }
        string Location { get; }
        Type[] GetTypes();
        AssemblyName[] GetReferencedAssemblies();
    }

    class AssemblyWrapper : IAssembly
    {
        public AssemblyWrapper(Assembly assembly)
        {
            Assembly = assembly;
        }

        public Assembly Assembly { get; }
        public string FullName => Assembly.FullName;
        public string Location => Assembly.Location;
        public Type[] GetTypes()
        {
            return Assembly.GetTypes();
        }

        public AssemblyName[] GetReferencedAssemblies()
        {
            return Assembly.GetReferencedAssemblies();
        }
    }
}