using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MarkUnit.Assemblies
{
    internal class AssemblyReader : IAssemblyReader
    {
        private readonly Dictionary<string, IAssembly> _assemblies = new Dictionary<string, IAssembly>();
        private readonly Dictionary<string, Assembly> _loadedAssemblies = new Dictionary<string, Assembly>();

        public AssemblyReader()
        {
            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        public string AssemblyPath { get; set; }

        public IAssembly LoadAssembly(string location)
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

        public void Loadall(Assembly assembly)
        {
            LoadAssembly(assembly.Location);
        }

        public IEnumerable<IAssembly> AllAssemblies => _assemblies.Values;

        public IAssembly LoadAssembly(Assembly assembly)
        {
            if (assembly == null) return null;
            if (!_assemblies.TryGetValue(assembly.FullName, out var result))
            {
                result = new ArchUnitAssembly(assembly);
                _assemblies.Add(assembly.FullName, result);
                result.ReferencedAssemblies = assembly.GetReferencedAssemblies().Select(LoadAssembly).Where(a => a != null).ToArray();
            }

            return result;
        }

        ~AssemblyReader()
        {
            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve -= CurrentDomain_AssemblyResolve;
        }

        protected IAssembly LoadAssembly(AssemblyName assemblyName)
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

        private void AddAssemblyToCache(Assembly assembly, string assemblyName)
        {
            if (assemblyName != null) TryAdd(assembly, assemblyName);
            TryAdd(assembly, assembly.FullName);
            TryAdd(assembly, assembly.Location);
        }

        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            return LoadAssemblyByFullName(args.Name);
        }

        private string GetAssemblyWithProperExtension(string path, string filename)
        {
            return Directory.EnumerateFiles(path).FirstOrDefault(n => MightBeAssembly(n, filename));
        }

        private string GetFullPathToAssembly(string assemblyName)
        {
            var p = AssemblyPath;
            var f = assemblyName.Split(',');
            var filename = GetAssemblyWithProperExtension(p, f[0]);
            return filename;
        }

        private bool IsCached(string assemblyName, out Assembly assembly)
        {
            return _loadedAssemblies.TryGetValue(assemblyName, out assembly);
        }

        private bool IsCompatibleInGAC(string assemblyName, out Assembly assembly)
        {
            try
            {
                assembly = Assembly.Load(new AssemblyName(assemblyName));
                if (IsInGAC(assembly.FullName, out assembly)) return true;
                throw new InvalidOperationException();
            }
            catch
            {
                Console.WriteLine("Could not load assembly: " + assemblyName);
                assembly = null;
                return false;
            }
        }

        private bool IsInAssemblyPath(string assemblyName, out Assembly assembly)
        {
            assembly = null;
            var filename = GetFullPathToAssembly(assemblyName);
            if (!string.IsNullOrEmpty(filename) && File.Exists(filename))
            {
                assembly = Assembly.ReflectionOnlyLoadFrom(filename);
                //     if (assembly.FullName != assemblyName) return false;
                AddAssemblyToCache(assembly, assemblyName);
                return true;
            }

            return false;
        }

        private bool IsInGAC(string assemblyName, out Assembly assembly)
        {
            try
            {
                assembly = Assembly.ReflectionOnlyLoad(assemblyName);
                AddAssemblyToCache(assembly, assemblyName);
                return true;
            }
            catch
            {
                assembly = null;
                return false;
            }
        }

        private Assembly LoadAssemblyByFullName(string assemblyName)
        {
            Assembly assembly;
            if (IsCached(assemblyName, out assembly)) return assembly;
            if (IsInAssemblyPath(assemblyName, out assembly)) return assembly;
            if (IsInGAC(assemblyName, out assembly)) return assembly;
            if (IsCompatibleInGAC(assemblyName, out assembly)) return assembly;
            return null;
        }

        private Assembly LoadAssemblyByLocation(string fullPathToAssembly)
        {
            if (_loadedAssemblies.TryGetValue(fullPathToAssembly, out var assembly) == false)
            {
                assembly = Assembly.ReflectionOnlyLoadFrom(fullPathToAssembly);
                AddAssemblyToCache(assembly, assembly.FullName);
            }

            return assembly;
        }

        private bool MightBeAssembly(string arg, string filename)
        {
            var l = filename.ToLower();
            var a = Path.GetFileName(arg.ToLower());
            return a == l + ".dll" || a == l + ".exe";
        }

        private void TryAdd(Assembly assembly, string key)
        {
            if (_loadedAssemblies.ContainsKey(key)) return;
            _loadedAssemblies.Add(key, assembly);
        }

        private IAssembly TryLoad(AssemblyName assemblyName)
        {
            if (_assemblies.TryGetValue(assemblyName.FullName, out var result))
                return result;

            var assembly = LoadAssemblyByFullName(assemblyName.FullName);
            return LoadAssembly(assembly);
        }
    }
}
