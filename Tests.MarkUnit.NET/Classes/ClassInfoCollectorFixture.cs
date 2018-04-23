using Microsoft.VisualStudio.TestTools.UnitTesting;
using MarkUnit.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarkUnit.Assemblies;
using Moq;

namespace Tests.MarkUnit.Classes
{
    [TestClass]
    public class ClassInfoCollectorFixture
    {
        private MarkUnitClass _classInfo;
        private ClassInfoCollector _sut;

        [TestInitialize]
        public void Setup()
        {
            var assemblyReaderMock = new Mock<IAssemblyReader>();
             _sut=new ClassInfoCollector(assemblyReaderMock.Object);

            var classToExamine = typeof(TestClass);
            IAssembly assembly=new MarkUnitAssembly(classToExamine.Assembly);
            _classInfo=new MarkUnitClass(assembly, classToExamine);

        }
        [TestMethod]
        public void Examine_Should_ExtractTypeInfoFromConstructors()
        {
         
            _sut.Examine(_classInfo);
            AssertReferencedClass<string>();
            AssertReferencedClass<int>();
        }

        private void AssertReferencedClass<T>()
        {
            Assert.IsTrue(_classInfo.ReferencedClasses.Any(c=>c.ClassType==typeof(T)));
        }

        [TestMethod]
        public void Examine_Should_ExtractTypeTypeInfoFromConstructorMethodBody()
        {
            _sut.Examine(_classInfo);
            AssertReferencedClass<char>();
        }
     
        [TestMethod]
        public void Examine_Should_ExtractTypeTypeInfoFromPrivateFields()
        {
            _sut.Examine(_classInfo);
            AssertReferencedClass<double>();
        }
 
        [TestMethod]
        public void Examine_Should_ExtractTypeTypeInfoFromPublicMethodParameters()
        {
            _sut.Examine(_classInfo);
            AssertReferencedClass<float>();
        }

        [TestMethod]
        public void Examine_Should_ExtractTypeTypeInfoFromPublicMethodReturnTypes()
        {
            _sut.Examine(_classInfo);
            AssertReferencedClass<byte>();
        }

        [TestMethod]
        public void Examine_Should_ExtractTypeTypeInfoFromNonPublicMethodParameters()
        {
            _sut.Examine(_classInfo);
            AssertReferencedClass<float[]>();
        }

        [TestMethod]
        public void Examine_Should_ExtractTypeTypeInfoFromNonMethodReturnTypes()
        {
            _sut.Examine(_classInfo);
            AssertReferencedClass<byte[]>();
        }

        class TestClass
        {
            private double _field;
            public TestClass(int p)
            {
                char c = p.ToString().First();
                _field = p.ToString().IndexOf(c);
            }

            private TestClass(string p) { }

            public byte PublicMethod(float f)
            {
                return 0;
            }

            protected byte[] NonPublicMethod(float[] f)
            {
                return new byte[0];
            }

        }
    }
}