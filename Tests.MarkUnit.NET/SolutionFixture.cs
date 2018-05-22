using Microsoft.VisualStudio.TestTools.UnitTesting;
using MarkUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarkUnit.Assemblies;
using Moq;

namespace Tests.MarkUnit
{
    [TestClass()]
    public class SolutionFixture
    {
        [TestMethod()]
        public void WithImmediateCheck_Should_Set_Static_ImmediateCheckField_To_True()
        {
            new Solution(new Mock<IAssemblyCollector>().Object).WithImmediateCheck();
            Assert.IsTrue(Instances.ImmediateCheck);
        }
    }
}