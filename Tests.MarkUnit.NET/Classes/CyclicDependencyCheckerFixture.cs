using System.Collections.Generic;
using System.Linq;
using System.Net;
using Castle.Core.Resource;
using MarkUnit.Assemblies;
using MarkUnit.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Tests.MarkUnit.Classes
{
    [TestClass]
    public class CyclicDependencyCheckerFixture
    {
        private static int cnt = 0;
        static string UniqueClassName()
        {
            return $"C{++cnt}";
        }
        [TestMethod]
        public void HasCyclicDependencies_Should_ReturnFalse_When_ClassHasNoCylcicDependencies()
        {
            var sut=new CyclicDependencyChecker();
            IAssembly assembly=new MarkUnitAssembly(GetType().Assembly);
            IClass classInfo = CreateSimpleClass("NameSpace");
            Assert.IsFalse(sut.HasCyclicDependencies(classInfo));
        }

        private IClass CreateSimpleClass(string nameSpace)
        {
            return Mock.Of<IClass>(
                c => c.FullName==UniqueClassName() &&
                     c.Namespace==nameSpace && 
                     c.ReferencedClasses == new List<IClass>() &&
                     c.ReferencedNameSpaces == new List<string>());

        }

        private IClass CreateClassWithReferences(string nameSpace,params IClass[] references)
        {
            var classes = new List<IClass>(references);
            var referencedNameSpaces = references.Select(c => c.Namespace).ToArray();
            return Mock.Of<IClass>(
                c => c.FullName==UniqueClassName() &&
                     c.Namespace==nameSpace && 
                     c.ReferencedClasses == classes &&
                     c.ReferencedNameSpaces == referencedNameSpaces);
        }

        [TestMethod]
        public void HasCyclicDependencies_Should_ReturnFalse_When_ClassReferencesSameNamespace()
        {
            string nameSpace = "N";
            var sut=new CyclicDependencyChecker();
            var classInfo=CreateClassWithReferences(nameSpace, CreateSimpleClass(nameSpace));

            Assert.IsFalse(sut.HasCyclicDependencies(classInfo));
        }

        [TestMethod]
        public void HasCyclicDependencies_Should_ReturnTrue_When_ReferencedClassReferencesSameNamespace()
        {
            string nameSpace = "N";
            string otherNameSpace = "N1";
            var sut=new CyclicDependencyChecker();

            var c1 = CreateSimpleClass(nameSpace);
            var c2 = CreateClassWithReferences(otherNameSpace, c1);
            var c3 = CreateClassWithReferences(nameSpace, c1,c2);

            Assert.IsTrue(sut.HasCyclicDependencies(c3));
        }

        [TestMethod]
        public void HasCyclicDependencies_Should_ReturnTrue_OnCircularClassReferences()
        {
            string nameSpace = "N";
            string otherNameSpace = "N1";
            var sut=new CyclicDependencyChecker();

            var r = new List<IClass>();
            var c4=Mock.Of<IClass>(
                c => c.FullName==UniqueClassName() &&
                     c.Namespace==nameSpace && 
                     c.ReferencedClasses == r &&
                     c.ReferencedNameSpaces == new[]{otherNameSpace});
        
            var c3 = CreateClassWithReferences(otherNameSpace, c4);
            var c2 = CreateClassWithReferences(otherNameSpace, c3);
            var c1 = CreateClassWithReferences(nameSpace, c2);
            r.Add(c1);

            Assert.IsTrue(sut.HasCyclicDependencies(c4));
        }
    }
}