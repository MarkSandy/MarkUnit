using System;
using System.Reflection;

namespace MarkUnit.Assemblies
{
    internal interface IAssemblyUtils
    {
        void DisableReflectionOnlyLoad();
        void EnableReflectionOnlyLoad(Func<object, ResolveEventArgs, Assembly> eventhandler);
        string GetAssemblyNameInDirectory(string path, string filename);
        Assembly Load(string assemblyName);
        Assembly LoadFrom(string fullPathName);
        Assembly LoadFromAssemblyName(string assemblyName);
    }
}
