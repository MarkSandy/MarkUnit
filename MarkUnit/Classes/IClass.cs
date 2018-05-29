using System;
using System.Collections.Generic;
using MarkUnit.Assemblies;

namespace MarkUnit.Classes
{
    public interface IType : INamedComponent
    {
        Type ClassType { get; }
        IAssembly Assembly { get; }
        string Namespace { get; }

    }
    public interface IClass : IType
    {
        IEnumerable<string> ReferencedNameSpaces { get; }
        IEnumerable<IClass> ReferencedClasses { get; }
        void AddReferencedClass(IClass referencedClass);
    }
}