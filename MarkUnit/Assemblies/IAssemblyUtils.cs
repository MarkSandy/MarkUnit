using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MarkUnit.Assemblies
{
    internal interface IAssemblyUtils
    {
        IAssembly LoadFrom(string fullPathName);
        IAssembly LoadFromAssemblyName(string assemblyName);
        IAssembly Load(string assemblyName);
        void EnableReflectionOnlyLoad(Func<object, ResolveEventArgs, Assembly> eventhandler);
        void DisableReflectionOnlyLoad();
        string GetAssemblyNameInDirectory(string path, string filename);
        bool FileExists(string filename);
    }

    internal class AssemblyUtils : IAssemblyUtils
    {
        private Func<object, ResolveEventArgs,Assembly> _eventHandler;

        public IAssembly LoadFrom(string fullPathName)
        {
            return new AssemblyWrapper(Assembly.ReflectionOnlyLoadFrom(fullPathName));
        }

        public IAssembly LoadFromAssemblyName(string assemblyName)
        {
            return new AssemblyWrapper(Assembly.Load(new AssemblyName(assemblyName)));
        }

        public IAssembly Load(string assemblyName)
        {
            return new AssemblyWrapper(Assembly.ReflectionOnlyLoad(assemblyName));
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

        public bool FileExists(string filename)
        {
            return !string.IsNullOrEmpty(filename) && File.Exists(filename);
        }

        private bool MightBeAssembly(string filename, string nameOfAssembly)
        {
            var lowerCaseAssemblyName = nameOfAssembly.ToLower();
            var lowerCasePlainFileName = Path.GetFileName(filename.ToLower());
            return lowerCasePlainFileName == lowerCaseAssemblyName + ".dll" || lowerCasePlainFileName == lowerCaseAssemblyName + ".exe";
        }


    }
}