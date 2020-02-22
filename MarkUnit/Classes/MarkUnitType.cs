using MarkUnit.Assemblies;
using System;

namespace MarkUnit.Classes
{
    internal class MarkUnitType : IType
    {
        public MarkUnitType(IAssembly assembly, Type type)
        {
            ClassType = type;
            Assembly = assembly;
            IsNative = type.IsNative();
        }

        public string Name => ClassType.Name;
        public Type ClassType { get; }
        public bool IsNative { get; }
        public IAssembly Assembly { get; }
        public string Namespace => ClassType.Namespace;
        public string FullName => ClassType.FullName;
    }

    internal static class TypeExtensions
    {
        public static bool IsNative(this Type type)
        {
            string nameSpace = type.Namespace ?? "";
            var scopeName = type.Module.ScopeName;
            return scopeName == "CommonLanguageRuntimeLibrary"
                   || scopeName.StartsWith("System")
                   || nameSpace.StartsWith("System")
                   || nameSpace.StartsWith("Microsoft");
        }
    }
}
