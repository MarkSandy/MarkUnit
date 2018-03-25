using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MarkUnit.Assemblies
{
    internal class DirectoryAssemblyCollector : IAssemblyCollector
    {
        private readonly List<IAssembly> _assembliesInDirectory = new List<IAssembly>();
        private readonly IAssemblyReader _assemblyReader;
        private string _path;

        public DirectoryAssemblyCollector(IAssemblyReader assemblyReader)
        {
            _assemblyReader = assemblyReader;
        }

        public string Path
        {
            get { return _path; }
            set
            {
                _path = value;
                _assemblyReader.AssemblyPath = Path;
            }
        }

        public string Pattern { get; set; }

        private bool MightBeAssembly(string arg)
        {
            return (arg.ToLower().EndsWith(".dll") || arg.ToLower().Matches(".exe")) && MatchesPattern(arg);
        }

        private bool MatchesPattern(string arg)
        {
            var filename = System.IO.Path.GetFileName(arg);
            return  filename.Matches(Pattern);
        }

        private void ReadAllAssembliesInDirectory()
        {
            Console.Write("read assemblies: ");
            foreach (var file in Directory.EnumerateFiles(Path).Where(MightBeAssembly))
            {
                try
                {
                    Console.Write(".");
                    _assembliesInDirectory.Add(_assemblyReader.LoadAssembly(file));
                }
                catch(Exception ex)
                {
                    Console.WriteLine("EXCEPTION Try Load "+file);
                    Console.WriteLine(ex.Message);
                }
            }
            Console.WriteLine();
        }

        public IEnumerable<IAssembly> Get()
        {
            if (_assemblyReader.AllAssemblies.Any() == false)
            {
                ReadAllAssembliesInDirectory();
            }

            return _assemblyReader.AllAssemblies;
        }

        public IEnumerable<IAssembly> SolutionAssemblies
        {
            get
            {
                if (_assembliesInDirectory.Count == 0)
                    ReadAllAssembliesInDirectory();

                return _assembliesInDirectory;
            }
        }
    }
}