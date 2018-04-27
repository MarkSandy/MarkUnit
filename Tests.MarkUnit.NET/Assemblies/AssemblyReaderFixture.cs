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

            var thisAssembly = CreateAssemblyMock("L", "A").Object;
            assemblyUtilMock.Setup(x => x.LoadFrom(fullPathNameToAssembly)).Returns(thisAssembly);
            var sut=new AssemblyReader(assemblyUtilMock.Object);
            
            // Act
            var actualAssembly=sut.LoadAssembly(fullPathNameToAssembly);
            
            // Assert
            Assert.AreSame(thisAssembly,actualAssembly.Assembly);
        }

        Mock<IAssembly> CreateAssemblyMock(string location, string fullName)
        {
            var assemblyMock=new Mock<IAssembly>();
            assemblyMock.SetupGet(a => a.FullName).Returns(fullName);
            assemblyMock.SetupGet(a => a.Location).Returns(location);
            return assemblyMock;
        }

        [TestMethod()]
        public void Loadall_Should_LoadAssembliesOnlyOnce()
        {
            string nameOfa1 = "A1";
            string nameOfa2 = "A2";
            var twoAssemblyNames = new[]
            {
                new AssemblyName(nameOfa1),
                new AssemblyName(nameOfa2),
            };
            
            var assemblyUtilMock = new Mock<IAssemblyUtils>();
            IAssembly a1=CreateAssemblyMock("L1",nameOfa1).Object;


            var a2Mock = CreateAssemblyMock("L2", nameOfa2);
            a2Mock.Setup(a => a.GetReferencedAssemblies()).Returns(twoAssemblyNames);
            IAssembly a2=a2Mock.Object;

            assemblyUtilMock.Setup(u => u.Load(a1.FullName)).Returns(a1);
            assemblyUtilMock.Setup(u => u.Load(a2.FullName)).Returns(a2);
            assemblyUtilMock.Setup(u => u.FileExists("filenameOfA1")).Returns(true);
            assemblyUtilMock.Setup(u => u.GetAssemblyNameInDirectory("AssemblyPath", a1.FullName)).Returns("filenameOfA1");
            assemblyUtilMock.Setup(u => u.LoadFrom("filenameOfA1")).Returns(a1);
            var sut=new AssemblyReader(assemblyUtilMock.Object);
            sut.AssemblyPath = "AssemblyPath";
            var assemblyMock = CreateAssemblyMock("Location", "Assembly");
            var assembly = assemblyMock.Object;
            
            assemblyUtilMock.Setup(a => a.LoadFrom(assembly.Location)).Returns(assemblyMock.Object);
            assemblyMock.Setup(a => a.GetReferencedAssemblies()).Returns(twoAssemblyNames);

            sut.Loadall(assembly);
            sut.Loadall(assembly);

            assemblyUtilMock.Verify(u=>u.LoadFrom(assembly.Location),Times.Once);
            assemblyUtilMock.Verify(u=>u.Load(a1.FullName),Times.Never);
            assemblyUtilMock.Verify(u=>u.LoadFrom("filenameOfA1"),Times.Once);
            assemblyUtilMock.Verify(u=>u.Load(a2.FullName),Times.Once);
        }

        [TestMethod()]
        public void LoadAssembly_Should_CreateAssemblyInfo_WhenCalledWithAssembly()
        {
            // Arrange
            var assemblyUtilMock = new Mock<IAssemblyUtils>();

            var thisAssembly = CreateAssemblyMock("L", "A").Object;
            var sut=new AssemblyReader(assemblyUtilMock.Object);
            
            // Act
            var actualAssembly=sut.LoadAssembly(thisAssembly);
            
            // Assert
            Assert.AreSame(thisAssembly,actualAssembly.Assembly);
        }
    }
}