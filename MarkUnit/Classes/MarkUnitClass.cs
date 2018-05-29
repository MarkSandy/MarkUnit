using System;
using System.Collections.Generic;
using System.Linq;
using MarkUnit.Assemblies;

namespace MarkUnit.Classes
{
    internal class MarkUnitType : IType
    {
        public MarkUnitType(IAssembly assembly, Type type)
        {
            ClassType = type;
            Assembly = assembly;
        }

        public string Name => ClassType.Name;
        public Type ClassType { get; }
        public IAssembly Assembly { get; }
        public string Namespace => ClassType.Namespace;
    }

    internal class MarkUnitClass
        : MarkUnitType,
          IClass
    {
        private readonly HashSet<IClass> _referencedClasses = new HashSet<IClass>();

        public MarkUnitClass(IAssembly assembly, Type type)
            : base(assembly, type) { }

        public IEnumerable<string> ReferencedNameSpaces => _referencedClasses.Select(c => c.Namespace).Distinct();
        public IEnumerable<IClass> ReferencedClasses => _referencedClasses;

        public void AddReferencedClass(IClass referencedClass)
        {
            _referencedClasses.Add(referencedClass);
        }
    }
}
