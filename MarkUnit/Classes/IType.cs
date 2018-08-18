using System;
using MarkUnit.Assemblies;

namespace MarkUnit.Classes
{
    public interface IType : INamedComponent
    {
        IAssembly Assembly { get; }
        Type ClassType { get; }
        string Namespace { get; }
    }
}
