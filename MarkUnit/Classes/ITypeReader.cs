using System;
using System.Collections.Generic;
using MarkUnit.Assemblies;

namespace MarkUnit.Classes
{
    internal interface ITypeReader
    {
        Predicate<Type> FilterFunc { get;set; }
        IEnumerable<IClass> LoadFromAssemblies(IFilteredAssemblies assemblies);
    }
}
