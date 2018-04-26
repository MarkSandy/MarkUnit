using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MarkUnit.Classes;

namespace MarkUnit.Assemblies
{
    internal class MarkUnitAssembly : IAssemblyInfo
    {
        private readonly List<IClassInfo> _classes = new List<IClassInfo>();

        public MarkUnitAssembly(Assembly assembly)
        {
            var a = assembly.FullName.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
            Name = a.FirstOrDefault();
            Assembly = assembly;
        }

        public IAssemblyInfo[] ReferencedAssemblies { get; set; }
        public Assembly Assembly { get; }

        public void AddClass(IClassInfo classType)
        {
            _classes.Add(classType);
        }

        public IEnumerable<IClassInfo> Classes => _classes;

        public string Name { get; }
    }
}
