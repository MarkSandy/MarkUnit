using Microsoft.VisualStudio.TestTools.UnitTesting;
using MarkUnit.Classes;
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
            var verifierMock=new Mock<IAssertionVerifier<IClass>>();
            var filterMock = new Mock<IFilter<IClass>>();
            verifierMock.SetupGet(v => v.Items).Returns(filterMock.Object);
            var sut=new InterfacePredicate<IClass>(verifierMock.Object);
            
            var v1 = sut.That();
            var v2 = sut.Which();

            Assert.AreSame(v1,v2);
        }
    }
}