using MarkUnit.Assemblies;
using System;

namespace MarkUnit.Classes
{
    public interface IType : INamedComponent
    {
        IAssembly Assembly { get; }
        Type ClassType { get; }
        bool IsNative { get; }
        string Namespace { get; }
        string FullName { get; }
    }
}
