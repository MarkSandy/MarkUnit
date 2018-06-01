using Microsoft.VisualStudio.TestTools.UnitTesting;
using MarkUnit.Classes;
using System.Collections.Generic;
using System.Linq;
using MarkUnit.Assemblies;
using Moq;

namespace Tests.MarkUnit.Classes
{
    [TestClass()]
    public class MarkUnitInterfaceFixture
    {
        [TestMethod()]
        public void AddReferencedClass_Should_AddItemToWrappedClass()
        {
            var iclassMock=new Mock<IClass>();
            var sut=new MarkUnitInterface(iclassMock.Object);

            var c1 = iclassMock.Object;
            sut.AddReferencedClass(c1);
            iclassMock.Verify(c=>c.AddReferencedClass(c1),Times.Once);
        }

        [TestMethod()]
        public void MarkUnitInterface_Should_WrapInterfaceProperties()
        {
            var thisClass = typeof(MarkUnitInterfaceFixture);
            var iclass=new MarkUnitClass(new MarkUnitAssembly(thisClass.Assembly),  thisClass);
             var sut=new MarkUnitInterface(iclass);
            Assert.AreEqual(sut.ClassType,iclass.ClassType);
            Assert.AreEqual(sut.Assembly, iclass.Assembly);
            Assert.IsTrue(Equivalent(sut.ReferencedNameSpaces, iclass.ReferencedNameSpaces));
            Assert.IsTrue(Equivalent(sut.ReferencedClasses, iclass.ReferencedClasses));
            Assert.AreEqual(sut.Namespace, iclass.Namespace);
            Assert.AreEqual(sut.Name, iclass.Name);
            
        }

        bool Equivalent<T>(IEnumerable<T> e1, IEnumerable<T> e2)
        {
            var a1 = e1.ToArray();
            var a2 = e2.ToArray();
            return a1.All(a2.Contains) && a2.All(a1.Contains);
        }
    }
}