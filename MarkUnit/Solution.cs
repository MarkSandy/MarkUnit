using System.Reflection;
using MarkUnit.Assemblies;
using MarkUnit.Classes;

namespace MarkUnit
{
    public class Solution
    {
        static Solution()
        {
        }

        public static Solution Create()
        {
            return new Solution(null);
        }

        public Solution FromMainAssembly(Assembly mainAssembly)
        {
            var assemblyCollector = Instances.AssemblyCollector;
            assemblyCollector.MainAssembly = new AssemblyWrapper(mainAssembly);
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
            _assemblyCollector.Pattern = pattern;
            return this;
        }

        public Solution WithImmediateCheck()
        {
            _immediateCheck = true;
            return this;
        }
        public static Solution Create(string path, string pattern=null)
        {
            var assemblyCollector = Instances.DirectoryAssemblyCollector;
            assemblyCollector.Path = path;
            assemblyCollector.Pattern = pattern;
            return new Solution(assemblyCollector);
        }

        private   IAssemblyCollector _assemblyCollector;
        private bool _immediateCheck=false;

        public Solution(IAssemblyCollector assemblyCollector)
        {
            _assemblyCollector = assemblyCollector;
        
        }

        private IAssemblyPredicate CreateAssembly(bool negate, bool not)
        {
            return new AssemblyPredicate(_assemblyCollector, negate,not);
        }

        public IAssemblyPredicate NoAssembly()
        {
            PredicateString.Start("No assembly");
            return CreateAssembly(true, false);
        }

        public IAssemblyPredicate EachAssembly()
        {
            PredicateString.Start("Each assembly");
            return CreateAssembly(false, false);
        }

        private IClassPredicate CreateClass(bool negate, bool not)
        {
            var classCollector = Instances.ClassCollector;
            classCollector.Assemblies=new FilteredAssemblies(_assemblyCollector.SolutionAssemblies);
            return new ClassPredicate(classCollector, negate,not);
        }

        public IClassPredicate OnlyAClass()
        {
            PredicateString.Start("Only a class");
            return CreateClass(true,true);
        }

        public IAssemblyPredicate OnlyAnAssembly()
        {
            PredicateString.Start("Only an assembly");
            return CreateAssembly(true,true);;
        }
        public IClassPredicate EachClass()
        {
            PredicateString.Start("Each class");
            return CreateClass(false,false);
        }

        public IClassPredicate NoClass()
        {
            PredicateString.Start("No class");
            return CreateClass(true,false);
        }
    }
}