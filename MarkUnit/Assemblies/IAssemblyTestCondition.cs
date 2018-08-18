﻿using System;
using System.Linq.Expressions;

namespace MarkUnit.Assemblies
{
    public interface IAssemblyTestCondition
    {
        IAssemblyRule HaveName(Expression<Predicate<string>> nameFilterExpression);
        IAssemblyTestCondition Not();
        IAssemblyRule ReferenceAssembliesMatching(string pattern);
        IAssemblyRule ReferenceAssembliesMatching(Expression<Predicate<IAssembly>> assemblyFilterExpression);
        IAssemblyRule ReferenceAssembly(string name);
    }
}
