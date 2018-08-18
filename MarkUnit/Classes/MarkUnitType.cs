using System;
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
}
