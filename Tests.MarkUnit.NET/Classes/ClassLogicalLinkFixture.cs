using Microsoft.VisualStudio.TestTools.UnitTesting;
using MarkUnit.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace Tests.MarkUnit.Classes
{
    [TestClass]
    public class ClassLogicalLinkFixture
    {
        [TestMethod]
        public void ClassLogicalLink_Check_Should_CallCheckOfFollowUp()
        {
            var followUpMock = new Mock<IInternalClassTestCondition>(MockBehavior.Loose);
            var sut=new ClassLogicalLink(followUpMock.Object);
            sut.Check();
            followUpMock.Verify(f=>f.Check(),Times.Once);
        }
    }
}