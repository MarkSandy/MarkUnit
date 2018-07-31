using Microsoft.VisualStudio.TestTools.UnitTesting;
using MarkUnit.Classes;
using System.Collections.Generic;
using System.Linq;
using MarkUnit.Assemblies;
using Moq;

namespace Tests.MarkUnit.Classes
{
    [TestClass()]
    public class ClassCollectorFixture
    {

        [TestMethod()]
        public void Get_Should_ExamineAllClassesFromAssemblies()
        {
            var assemblyFilter=new Mock<IFilteredAssemblies>().Object; 
            var classReaderMock = new Mock<ITypeReader<IInternalClass>>();
            var c1 = new Mock<IInternalClass>().Object;
            var c2 = new Mock<IInternalClass>().Object;
            var classes=new IInternalClass[]{c1,c2};
            classReaderMock.Setup(c => c.LoadFromAssemblies(assemblyFilter)).Returns(classes);
            var classInfoCollectorMock = new Mock<IClassInfoCollector>();
            var examinedClasses = new List<IClass>();
            classInfoCollectorMock.Setup(c => c.Examine(It.IsAny<IInternalClass>())).Returns<IInternalClass>(c =>
            {
                
                examinedClasses.Add(c);
                return c;
            });
            

            var sut=new ClassCollector(classReaderMock.Object,classInfoCollectorMock.Object);
            sut.Assemblies=assemblyFilter;

            var actualClasses=sut.Get().ToArray();
            Assert.IsTrue(examinedClasses.Count==2);
            Assert.IsTrue(examinedClasses.Contains(c1));
            Assert.IsTrue(examinedClasses.Contains(c2));

            Assert.IsTrue(actualClasses.Length==2);
            Assert.IsTrue(actualClasses.Contains(c1));
            Assert.IsTrue(actualClasses.Contains(c2));

        }
    }
}