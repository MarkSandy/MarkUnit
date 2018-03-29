﻿using System.Reflection;
using MarkUnit.Assemblies;
using MarkUnit.Classes;

namespace MarkUnit
{
    public class Solution
    {
        static Solution()
        {
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

        public static Solution Create(string path, string pattern=null)
        {
            var assemblyCollector = Instances.DirectoryAssemblyCollector;
            assemblyCollector.Path = path;
            assemblyCollector.Pattern = pattern;
            return new Solution(assemblyCollector);
        }

        private   IAssemblyCollector _assemblyCollector;
        private string _pattern;

        public Solution(IAssemblyCollector assemblyCollector)
        {
            _assemblyCollector = assemblyCollector;
        
        }

        private IAssemblyPredicate CreateAssembly(bool negate)
        {
            return new AssemblyPredicate(_assemblyCollector, negate);
        }

        public IAssemblyPredicate NoAssembly()
        {
            PredicateString.Start("No assembly");
            return CreateAssembly(true);
        }

        public IAssemblyPredicate EachAssembly()
        {
            PredicateString.Start("Each assembly");
            return CreateAssembly(false);
        }

        private IClassPredicate CreateClass(bool negate)
        {
            var classCollector = Instances.ClassCollector;
            classCollector.Assemblies=new FilteredAssemblies(_assemblyCollector.SolutionAssemblies);
            return new ClassPredicate(classCollector, negate);
        }


        public IClassPredicate EachClass()
        {
            PredicateString.Start("Each class");
            return CreateClass(false);
        }

        public IClassPredicate NoClass()
        {
            PredicateString.Start("No class");
            return CreateClass(true);
        }
    }
}