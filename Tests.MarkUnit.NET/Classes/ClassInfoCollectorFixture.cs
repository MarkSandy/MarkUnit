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
    [TestClass()]
    public class ClassInfoCollectorFixture
    {
       
        [TestMethod()]
        public void Examine_Should_()
        {
            var assemblyReaderMock = new Mock<IAssemblyReader>();
            var sut=new ClassInfoCollector(assemblyReaderMock.Object);
            
            // sut.Examine(classInfo);
            Assert.Inconclusive("To be written");
        }
    }
}