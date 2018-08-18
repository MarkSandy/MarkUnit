using MarkUnit.Assemblies;
using MarkUnit.Classes;

namespace MarkUnit
{
    /// <summary>
    ///     We could use a proper DI container, but for now we want to avoid it.
    /// </summary>
    internal static class Instances
    {
        private static IAssemblyReader _assemblyReader;
        private static ITypeReader<IInternalClass> _classReader;
        private static ITypeReader<IType> _typeReader;
        private static IClassCollector _classCollector;
        private static ITypeCollector _typeCollector;
        private static IInterfaceRuleFactory _interfaceRuleFactory;
        private static IClassRuleFactory _classRuleFactory;
        private static ITypeRuleFactory _typeRuleFactory;

        public static bool ThrowException = true;
        public static bool ImmediateCheck = false;

        public static IClassCollector ClassCollector => _classCollector ?? (_classCollector = new ClassCollector(ClassReader, ClassInfoCollector));

        public static ITypeCollector TypeCollector => _typeCollector ?? (_typeCollector = new TypeCollector(TypeReader));

        public static IInterfaceRuleFactory InterfaceRuleFactory => _interfaceRuleFactory ?? (_interfaceRuleFactory = new InterfaceRuleFactory());

        public static IClassRuleFactory ClassRuleFactory => _classRuleFactory ?? (_classRuleFactory = new ClassRuleFactory());

        public static ITypeRuleFactory TypeRuleFactory => _typeRuleFactory ?? (_typeRuleFactory = new TypeRuleFactory());

        private static ITypeReader<IType> TypeReader => _typeReader ?? (_typeReader = new TypeReader());

        private static IAssemblyReader AssemblyReader => _assemblyReader ?? (_assemblyReader = new AssemblyReader(new AssemblyUtils()));

        private static ITypeReader<IInternalClass> ClassReader => _classReader ?? (_classReader = new ClassReader(TypeReader));

        public static AssemblyCollector AssemblyCollector => new AssemblyCollector(AssemblyReader);

        public static DirectoryAssemblyCollector DirectoryAssemblyCollector => new DirectoryAssemblyCollector(AssemblyReader);

        private static IClassInfoCollector ClassInfoCollector => new ClassInfoCollector(AssemblyReader);

        public static IAssertionVerifier<T> CreateAssertionVerifier<T>(IFilter<T> items, IFilter<T> assertions, bool negateAssertion)
            where T : INamedComponent
        {
            ITestResultLogger testResultLogger;
            if (ThrowException)
                testResultLogger = new TestResultExceptionLogger();
            else
                testResultLogger = new TestResultLogger();
            if (!ImmediateCheck)
                return new AssertionVerifier<T>(items, assertions, negateAssertion, testResultLogger);
            return new ImmediateCheckAssertionVerifier<T>(items, assertions, negateAssertion, testResultLogger);
        }
    }
}
