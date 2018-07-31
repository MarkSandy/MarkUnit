using System;
using System.Collections.Generic;
using System.Linq;
using MarkUnit.Assemblies;

namespace MarkUnit.Classes
{
    internal class MarkUnitClass
        : MarkUnitType,
          IInternalClass
    {
        private readonly HashSet<IClass> _referencedClasses = new HashSet<IClass>();
        private readonly List<IMethod> _methods=new List<IMethod>();
        private readonly List<IMethod> _constructors=new List<IMethod>();

        public MarkUnitClass(IAssembly assembly, Type type)
            : base(assembly, type) { }

        public IEnumerable<string> ReferencedNameSpaces => _referencedClasses.Select(c => c.Namespace).Distinct();
        public IEnumerable<IClass> ReferencedClasses => _referencedClasses;

        public void AddReferencedClass(IClass referencedClass)
        {
            _referencedClasses.Add(referencedClass);
        }

        public IEnumerable<IMethod> Methods => _methods;

        public IEnumerable<IMethod> Constructors => _constructors;

        public void AddMethod(IMethod method)
        {
            _methods.Add(method);
        }

        public void AddConstructor(IMethod constructor)
        {
            _constructors.Add(constructor);
        }
    }
}
