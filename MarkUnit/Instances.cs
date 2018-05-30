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
        private static ITypeReader<IClass> _classReader;
        private static ITypeReader<IType> _typeReader;

        public static IAssemblyReader AssemblyReader => _assemblyReader ?? (_assemblyReader = new AssemblyReader(new AssemblyUtils()));

        public static ITypeReader<IClass> ClassReader => _classReader ?? (_classReader = new ClassReader());
        public static ITypeReader<IType> TypeReader => _typeReader ?? (_typeReader = new TypeReader());

        public static AssemblyCollector AssemblyCollector=>new AssemblyCollector(AssemblyReader);
        
        public static DirectoryAssemblyCollector DirectoryAssemblyCollector=>new DirectoryAssemblyCollector(AssemblyReader);
        
        public static IClassInfoCollector ClassInfoCollector=>new ClassInfoCollector(AssemblyReader);
     
        public static IClassCollector ClassCollector=new ClassCollector(ClassReader, ClassInfoCollector);
        public static ITypeCollector TypeCollector=new TypeCollector(TypeReader);
        public static IAssertionVerifier<T> CreateAssertionVerifier<T>(IFilter<T> items, IFilter<T> assertions, bool negateAssertion) where T : INamedComponent
        {
            ITestResultLogger testResultLogger=new TestResultLogger();
            if (!ImmediateCheck)
                return new AssertionVerifier<T>(items, assertions, negateAssertion, testResultLogger);
            else
                return new ImmediateCheckAssertionVerifier<T>(items, assertions, negateAssertion, testResultLogger);
        }

        public static bool ImmediateCheck { get; set; } = false;
    }
}
