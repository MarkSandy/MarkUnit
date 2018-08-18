using System.Reflection;
using MarkUnit.Assemblies;
using MarkUnit.Classes;

namespace MarkUnit
{
    public class Solution
    {
        private IAssemblyCollector _assemblyCollector;
        private string _pattern;

        public Solution(IAssemblyCollector assemblyCollector)
        {
            _assemblyCollector = assemblyCollector;
        }

        public static Solution Create(Assembly mainAssembly)
        {
            var assemblyCollector = Instances.AssemblyCollector;
            assemblyCollector.MainAssembly = mainAssembly;
            return new Solution(assemblyCollector);
        }

        public static Solution Create()
        {
            return new Solution(null);
        }

        public static Solution Create(string path, string pattern = null)
        {
            var assemblyCollector = Instances.DirectoryAssemblyCollector;
            assemblyCollector.Path = path;
            assemblyCollector.Pattern = pattern;
            return new Solution(assemblyCollector);
        }

        public IAssemblyPredicate EachAssembly()
        {
            PredicateString.Start("Each assembly");
            return CreateAssembly(false, false);
        }

        public IClassPredicate EachClass()
        {
            PredicateString.Start("Each class");
            return CreateClass(false, false);
        }

        public ITypePredicate EachType()
        {
            PredicateString.Start("Each type");
            return CreateType(false, false);
        }

        public Solution FromMainAssembly(Assembly mainAssembly)
        {
            var assemblyCollector = Instances.AssemblyCollector;
            assemblyCollector.MainAssembly = mainAssembly;
            _assemblyCollector = assemblyCollector;
            return this;
        }

        public Solution FromPath(string path)
        {
            var directoryAssemblyCollector = Instances.DirectoryAssemblyCollector;
            directoryAssemblyCollector.Path = path;
            _assemblyCollector = directoryAssemblyCollector;
            return this;
        }

        public Solution Matching(string pattern)
        {
            _pattern = pattern;
            _assemblyCollector.Pattern = pattern;
            return this;
        }

        public IAssemblyPredicate NoAssembly()
        {
            PredicateString.Start("No assembly");
            return CreateAssembly(true, false);
        }

        public IClassPredicate NoClass()
        {
            PredicateString.Start("No class");
            return CreateClass(true, false);
        }

        public ITypePredicate NoType()
        {
            PredicateString.Start("No type");
            return CreateType(true, false);
        }

        public IClassPredicate OnlyAClass()
        {
            PredicateString.Start("Only a class");
            return CreateClass(true, true);
        }

        public IAssemblyPredicate OnlyAnAssembly()
        {
            PredicateString.Start("Only an assembly");
            return CreateAssembly(true, true);
            ;
        }

        public ITypePredicate OnlyAType()
        {
            PredicateString.Start("Only a type");
            return CreateType(true, true);
        }

        public Solution WithImmediateCheck()
        {
            Instances.ImmediateCheck = true;
            return this;
        }

        public Solution WithoutException()
        {
            Instances.ThrowException = false;
            return this;
        }

        private IAssemblyPredicate CreateAssembly(bool negate, bool not)
        {
            return new AssemblyPredicate(_assemblyCollector, negate, not);
        }

        private IClassPredicate CreateClass(bool negate, bool not)
        {
            var classCollector = Instances.ClassCollector;
            classCollector.Assemblies = new FilteredAssemblies(_assemblyCollector.SolutionAssemblies);
            return new ClassPredicate(classCollector, negate, not);
        }

        private ITypePredicate CreateType(bool negate, bool not)
        {
            var typeCollector = Instances.TypeCollector;
            typeCollector.Assemblies = new FilteredAssemblies(_assemblyCollector.SolutionAssemblies);
            return new TypePredicate(typeCollector, negate, not);
        }
    }
}
