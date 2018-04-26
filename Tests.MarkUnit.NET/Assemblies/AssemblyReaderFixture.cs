using Microsoft.VisualStudio.TestTools.UnitTesting;
using MarkUnit.Assemblies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace Tests.MarkUnit.Assemblies
{
    [TestClass()]
    public class AssemblyReaderFixture
    {
        [TestMethod()]
        public void AssemblyReader_Should_EnableReflectionOnlyLoad()
        {
            var assemblyUtilMock = new Mock<IAssemblyUtils>();
            var sut=new AssemblyReader(assemblyUtilMock.Object);
            assemblyUtilMock.Verify(x=>x.EnableReflectionOnlyLoad(It.IsAny<Func<object,ResolveEventArgs,Assembly>>()),Times.Once);
        }

        [TestMethod()]
        public void LoadAssembly_Should_LoadAssemblyByPathName_WhenCalledWithString()
        {
            // Arrange
            var assemblyUtilMock = new Mock<IAssemblyUtils>();
            string fullPathNameToAssembly="Location";
            Assembly thisAssembly=GetType().Assembly;
            assemblyUtilMock.Setup(x => x.LoadFrom(fullPathNameToAssembly)).Returns(thisAssembly);
            var sut=new AssemblyReader(assemblyUtilMock.Object);
            
            // Act
            var actualAssembly=sut.LoadAssembly(fullPathNameToAssembly);
            
            // Assert
            Assert.AreSame(thisAssembly,actualAssembly.Assembly);
        }

        [TestMethod()]
        public void Loadall_Should_()
        {
            var assemblyUtilMock = new Mock<IAssemblyUtils>();
            var sut=new AssemblyReader(assemblyUtilMock.Object);
//            sut.Loadall();
        }

        [TestMethod()]
        public void LoadAssembly_Should_CreateWrapperAroundAssembly_WhenCalledWithAssembly()
        {
            // Arrange
            var assemblyUtilMock = new Mock<IAssemblyUtils>();
            var thisAssembly=GetType().Assembly;
            var sut=new AssemblyReader(assemblyUtilMock.Object);
            
            // Act
            var actualAssembly=sut.LoadAssembly(thisAssembly);
            
            // Assert
            Assert.AreSame(thisAssembly,actualAssembly.Assembly);
        }
    }
}