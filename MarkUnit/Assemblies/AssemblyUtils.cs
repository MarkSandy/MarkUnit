using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MarkUnit.Assemblies
{
    internal class AssemblyUtils : IAssemblyUtils
    {
        private Func<object, ResolveEventArgs,Assembly> _eventHandler;

        public Assembly LoadFrom(string fullPathName)
        {
            return Assembly.ReflectionOnlyLoadFrom(fullPathName);
        }

        public Assembly LoadFromAssemblyName(string assemblyName)
        {
            return Assembly.Load(new AssemblyName(assemblyName));
        }

        public Assembly Load(string assemblyName)
        {
            return Assembly.ReflectionOnlyLoad(assemblyName);
        }

        public void EnableReflectionOnlyLoad(Func<object, ResolveEventArgs,Assembly> eventhandler)
        {
            _eventHandler = eventhandler;
            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            return _eventHandler(sender, args);
        }

        public void DisableReflectionOnlyLoad()
        {
            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve -= CurrentDomain_AssemblyResolve;
        }

        public string GetAssemblyNameInDirectory(string path, string filename)
        {
            return Directory.EnumerateFiles(path).FirstOrDefault(n => MightBeAssembly(n, filename));
        }

        private bool MightBeAssembly(string filename, string nameOfAssembly)
        {
            var lowerCaseAssemblyName = nameOfAssembly.ToLower();
            var lowerCasePlainFileName = Path.GetFileName(filename.ToLower());
            return lowerCasePlainFileName == lowerCaseAssemblyName + ".dll" || lowerCasePlainFileName == lowerCaseAssemblyName + ".exe";
        }


    }
}