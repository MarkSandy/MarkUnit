using System;
using System.Collections.Generic;
using System.Linq;
using MarkUnit.Assemblies;

namespace MarkUnit.Classes
{
    internal class MarkUnitClass : IClassInfo
    {
        private readonly HashSet<IClassInfo> _referencedClasses = new HashSet<IClassInfo>();

        public MarkUnitClass(IAssemblyInfo assembly, Type type)
        {
            ClassType = type;
            Assembly = assembly;
        }

        public string Name => ClassType.Name;
        public Type ClassType { get; }
        public IAssemblyInfo Assembly { get; }
        public IEnumerable<string> ReferencedNameSpaces => _referencedClasses.Select(c => c.Namespace).Distinct();
        public IEnumerable<IClassInfo> ReferencedClasses => _referencedClasses;

        public string Namespace => ClassType.Namespace;

        public void AddReferencedClass(IClassInfo referencedClass)
        {
            _referencedClasses.Add(referencedClass);
        }
    }
}
