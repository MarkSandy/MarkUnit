using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MarkUnit.Assemblies
{
    internal class AssemblyReader : IAssemblyReader
    {
        private readonly Dictionary<string, IAssemblyInfo> _assemblies = new Dictionary<string, IAssemblyInfo>();
        private readonly Dictionary<string, IAssembly> _loadedAssemblies = new Dictionary<string, IAssembly>();

        private readonly IAssemblyUtils _assemblyUtils;

        public AssemblyReader(IAssemblyUtils assemblyUtils)
        {
            _assemblyUtils = assemblyUtils;
            _assemblyUtils.EnableReflectionOnlyLoad(CurrentDomain_AssemblyResolve);
        }

        ~AssemblyReader()
        {
            _assemblyUtils.DisableReflectionOnlyLoad();
        }

        protected IAssemblyInfo LoadAssembly(AssemblyName assemblyName)
        {
            try
            {
                return TryLoad(assemblyName);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private void AddAssemblyToCache(IAssembly assembly, string assemblyName)
        {
            if (assemblyName != null) TryAdd(assembly, assemblyName);
            TryAdd(assembly, assembly.FullName);
            TryAdd(assembly, assembly.Location);
        }

        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            return LoadAssemblyByFullName(args.Name).Assembly;
        }

        private string GetFullPathToAssembly(string assemblyName)
        {
            var f = assemblyName.Split(',');
            var filename = _assemblyUtils.GetAssemblyNameInDirectory(AssemblyPath, f.First());
            return filename;
        }

        private bool IsCached(string assemblyName, out IAssembly assembly)
        {
            return _loadedAssemblies.TryGetValue(assemblyName, out assembly);
        }

        private bool IsCompatibleAssemblyInGac(string assemblyName, out IAssembly assembly)
        {
            try
            {
                assembly = _assemblyUtils.LoadFromAssemblyName(assemblyName);
                if (IsInGac(assembly.FullName, out assembly)) return true;
                throw new InvalidOperationException();
            }
            catch
            {
                Console.WriteLine("Could not load assembly: " + assemblyName);
                assembly = null;
                return false;
            }
        }

        private bool IsInAssemblyPath(string assemblyName, out IAssembly assembly)
        {
            assembly = null;
            var filename = GetFullPathToAssembly(assemblyName);
            if (_assemblyUtils.FileExists(filename))
            {
                assembly =  _assemblyUtils.LoadFrom(filename);
                AddAssemblyToCache(assembly, assemblyName);
                return true;
            }

            return false;
        }

        private bool IsInGac(string assemblyName, out IAssembly assembly)
        {
            try
            {
                assembly = _assemblyUtils.Load(assemblyName);
                AddAssemblyToCache(assembly, assemblyName);
                return true;
            }
            catch
            {
                assembly = null;
                return false;
            }
        }

        private IAssembly LoadAssemblyByFullName(string assemblyName)
        {
            if (IsCached(assemblyName, out var assembly)) 
                return assembly;
            if (IsInAssemblyPath(assemblyName, out assembly)) 
                return assembly;
            if (IsInGac(assemblyName, out assembly)) 
                return assembly;
            if (IsCompatibleAssemblyInGac(assemblyName, out assembly)) 
                return assembly;
            return null;
        }

        private IAssembly LoadAssemblyByLocation(string fullPathToAssembly)
        {
            if (_loadedAssemblies.TryGetValue(fullPathToAssembly, out var assembly) == false)
            {
                assembly = _assemblyUtils.LoadFrom(fullPathToAssembly);
                AddAssemblyToCache(assembly, assembly.FullName);
            }

            return assembly;
        }

        private void TryAdd(IAssembly assembly, string key)
        {
            if (_loadedAssemblies.ContainsKey(key)) return;
            _loadedAssemblies.Add(key, assembly);
        }

        private IAssemblyInfo TryLoad(AssemblyName assemblyName)
        {
            if (_assemblies.TryGetValue(assemblyName.FullName, out var result))
                return result;

            var assembly = LoadAssemblyByFullName(assemblyName.FullName);
            return LoadAssembly(assembly);
        }

        public string AssemblyPath { get; set; }

        public IAssemblyInfo LoadAssembly(string location)
        {
            try
            {
                return LoadAssembly(LoadAssemblyByLocation(location));
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public void Loadall(IAssembly assembly)
        {
            LoadAssembly(assembly.Location);
        }

        public IEnumerable<IAssemblyInfo> AllAssemblies => _assemblies.Values;

        public IAssemblyInfo LoadAssembly(IAssembly assembly)
        {
            if (assembly == null) return null;
            if (!_assemblies.TryGetValue(assembly.FullName, out var result))
            {
                result = new MarkUnitAssembly(assembly);
                _assemblies.Add(assembly.FullName, result);
                result.ReferencedAssemblies = assembly.GetReferencedAssemblies().Select(LoadAssembly).Where(a => a != null).ToArray();
            }

            return result;
        }
    }
}