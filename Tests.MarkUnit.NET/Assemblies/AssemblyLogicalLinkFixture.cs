using MarkUnit.Assemblies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Tests.MarkUnit.Assemblies
{
    [TestClass]
    public class AssemblyLogicalLinkFixture
    {
        [TestMethod]
        public void Check_Should_CallCheckOfFollowUp()
        {
            var followUpMock = new Mock<IInternalAssemblyTestCondition>(MockBehavior.Loose);
            var sut=new AssemblyLogicalLink(followUpMock.Object);
            sut.Check();
            followUpMock.Verify(f=>f.Check(),Times.Once);
        }
    }
}