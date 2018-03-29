using System;
using System.Collections.Generic;
using MarkUnit.Assemblies;

namespace MarkUnit.Classes
{
    public interface IInterface : IClass { }

    internal class MarkUnitInterface : IInterface
    {
        private readonly IClass _classWrapper;
        public string Name => _classWrapper.Name;

        public Type ClassType => _classWrapper.ClassType;

        public IAssembly Assembly => _classWrapper.Assembly;

        public IEnumerable<string> ReferencedNameSpaces => _classWrapper.ReferencedNameSpaces;

        public IEnumerable<IClass> ReferencedClasses => _classWrapper.ReferencedClasses;

        public void AddReferencedClass(IClass referencedClass)
        {
            _classWrapper.AddReferencedClass(referencedClass);
        }

        public MarkUnitInterface(IClass classWrapper)
        {
            _classWrapper = classWrapper;
        }
    }
}
