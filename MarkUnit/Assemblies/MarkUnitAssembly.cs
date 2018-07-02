using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MarkUnit.Classes;

namespace MarkUnit.Assemblies
{
    internal class MarkUnitAssembly : IAssembly
    {
        private readonly List<IClass> _classes = new List<IClass>();

        public MarkUnitAssembly(Assembly assembly)
        {
            var a = assembly.FullName.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
            Name = a.FirstOrDefault();
            Assembly = assembly;
        }

        public IAssembly[] ReferencedAssemblies { get; set; }
        public Assembly Assembly { get; }

        public void AddClass(IClass classType)
        {
            _classes.Add(classType);
        }

        public IEnumerable<IClass> Classes => _classes;

        public string Name { get; }
    }
}
