using System;
using System.Collections.Generic;
using MarkUnit.Assemblies;

namespace MarkUnit.Classes
{
    public interface IInterface : IClassInfo { }

    internal class MarkUnitInterface : IInterface
    {
        private readonly IClassInfo _classWrapper;
        public string Name => _classWrapper.Name;

        public Type ClassType => _classWrapper.ClassType;

        public IAssemblyInfo AssemblyInfo => _classWrapper.AssemblyInfo;

        public IEnumerable<string> ReferencedNameSpaces => _classWrapper.ReferencedNameSpaces;

        public IEnumerable<IClassInfo> ReferencedClasses => _classWrapper.ReferencedClasses;

        public string Namespace => ClassType.Namespace;

        public void AddReferencedClass(IClassInfo referencedClass)
        {
            _classWrapper.AddReferencedClass(referencedClass);
        }

        public MarkUnitInterface(IClassInfo classWrapper)
        {
            _classWrapper = classWrapper;
        }
    }
}
