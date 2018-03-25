using System;
using System.Collections.Generic;
using System.Linq;
using MarkUnit.Assemblies;

namespace MarkUnit.Classes
{
    internal class ArchNetClass : IClass
    {
        private readonly HashSet<IClass> _referencedClasses = new HashSet<IClass>();
        public ArchNetClass(IAssembly assembly,Type type)
        {
            ClassType = type;
            Assembly = assembly;
        }
        
        public string Name => ClassType.Name;
        public Type ClassType { get; }
        public IAssembly Assembly { get; }
        public IEnumerable<string> ReferencedNameSpaces => _referencedClasses.Select(c => c.ClassType.Namespace).Distinct();
        public IEnumerable<IClass> ReferencedClasses => _referencedClasses;
        public void AddReferencedClass(IClass referencedClass)
        {
            _referencedClasses.Add(referencedClass);
        }
    }
}