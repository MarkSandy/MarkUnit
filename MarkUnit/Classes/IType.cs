using System;
using MarkUnit.Assemblies;

namespace MarkUnit.Classes
{
    public interface IType : INamedComponent
    {
        Type ClassType { get; }
        IAssembly Assembly { get; }
        string Namespace { get; }

    }
}