using Microsoft.VisualStudio.TestTools.UnitTesting;
using MarkUnit.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarkUnit;
using Moq;

namespace Tests.MarkUnit.Classes
{
    [TestClass()]
    public class InterfacePredicateFixture
    {
        [TestMethod()]
        public void That_Should_ReturnTheSameAsWith()
        {
            var verifierMock=new Mock<IAssertionVerifier<IClassInfo>>();
            var filterMock = new Mock<IFilter<IClassInfo>>();
            verifierMock.SetupGet(v => v.Items).Returns(filterMock.Object);
            var sut=new InterfacePredicate(verifierMock.Object);
            
            var v1 = sut.That();
            var v2 = sut.Which();

            Assert.AreSame(v1,v2);
        }
    }
}