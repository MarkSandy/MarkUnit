using System;
using System.Collections.Generic;
using MarkUnit.Assemblies;

namespace MarkUnit.Classes
{
    internal class MarkUnitInterface : IInterface, IInternalClass
    {
        private readonly IClass _classWrapper;
        private readonly List<IMethod> _methods = new List<IMethod>();
        private readonly List<IMethod> _constructors = new List<IMethod>();
        public string Name => _classWrapper.Name;

        public Type ClassType => _classWrapper.ClassType;

        public IAssembly Assembly => _classWrapper.Assembly;

        public IEnumerable<string> ReferencedNameSpaces => _classWrapper.ReferencedNameSpaces;

        public IEnumerable<IClass> ReferencedClasses => _classWrapper.ReferencedClasses;

        public string Namespace => ClassType.Namespace;

        public void AddReferencedClass(IClass referencedClass)
        {
            _classWrapper.AddReferencedClass(referencedClass);
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

        public MarkUnitInterface(IClass classWrapper)
        {
            _classWrapper = classWrapper;
        }
    }
}