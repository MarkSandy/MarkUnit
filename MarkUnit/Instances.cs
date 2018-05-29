using System;
using System.Collections.Generic;
using System.Dynamic;
using MarkUnit.Assemblies;
using MarkUnit.Classes;

namespace MarkUnit
{
    /// <summary>
    /// We could use a proper DI container, but for now we want to avoid it.
    /// </summary>
    internal class Instances
    {
        private static IAssemblyReader _assemblyReader;
        private static ITypeReader _classReader;

        public static IAssemblyReader AssemblyReader => _assemblyReader ?? (_assemblyReader = new AssemblyReader(new AssemblyUtils()));

        public static ITypeReader ClassReader => _classReader ?? (_classReader = new ClassReader());

        public static AssemblyCollector AssemblyCollector=>new AssemblyCollector(AssemblyReader);
        
        public static DirectoryAssemblyCollector DirectoryAssemblyCollector=>new DirectoryAssemblyCollector(AssemblyReader);
        
        public static IClassInfoCollector ClassInfoCollector=>new ClassInfoCollector(AssemblyReader);
     
        public static IClassCollector ClassCollector=new ClassCollector(ClassReader, ClassInfoCollector);
        public static ITypeCollector TypeCollector=new TypeCollector(ClassReader);
    }
}
