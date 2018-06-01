using Microsoft.VisualStudio.TestTools.UnitTesting;
using MarkUnit.Classes;
using Moq;

namespace Tests.MarkUnit.Classes
{
    [TestClass]
    public class ClassLogicalLinkFixture
    {
        [TestMethod]
        public void Check_Should_CallCheckOfFollowUp()
        {
            var followUpMock = new Mock<IInternalClassTestCondition>(MockBehavior.Loose);
            var sut=new ClassLogicalLink(followUpMock.Object);
            sut.Check();
            followUpMock.Verify(f=>f.Check(),Times.Once);
        }
    }

    [TestClass]
    public class InterfaceLogicalLinkFixture
    {
        [TestMethod]
        public void Check_Should_CallCheckOfFollowUp()
        {
            var followUpMock = new Mock<IInternalInterfaceTestCondition>(MockBehavior.Loose);
            var sut=new InterfaceLogicalLink(followUpMock.Object);
            sut.Check();
            followUpMock.Verify(f=>f.Check(),Times.Once);
        }
    }
}