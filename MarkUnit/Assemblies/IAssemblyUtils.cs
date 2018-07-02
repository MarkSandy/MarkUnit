using System;
using System.Reflection;

namespace MarkUnit.Assemblies
{
    internal interface IAssemblyUtils
    {
        Assembly LoadFrom(string fullPathName);
        Assembly LoadFromAssemblyName(string assemblyName);
        Assembly Load(string assemblyName);
        void EnableReflectionOnlyLoad(Func<object, ResolveEventArgs,Assembly> eventhandler);
        void DisableReflectionOnlyLoad();
        string GetAssemblyNameInDirectory(string path, string filename);
    }
}