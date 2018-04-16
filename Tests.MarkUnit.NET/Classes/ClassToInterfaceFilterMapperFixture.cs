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
    public class ClassToInterfaceFilterMapperFixture
    {
        
        [TestMethod()]
        public void AppendCondition_Should_AppendPredicateToVerifier()
        {
            var classFilterMock = new Mock<IFilter<IClass>>();
            var sut = new ClassToInterfaceFilterMapper(classFilterMock.Object);
            sut.AppendCondition(i=>true);
            classFilterMock.Verify(f=>f.AppendCondition(It.IsAny<Predicate<IClass>>()));
        }

        [TestMethod()]
        public void Materialize_Should_MaterializeFilter()
        {
            var classFilterMock = new Mock<IFilter<IClass>>();
            var sut = new ClassToInterfaceFilterMapper(classFilterMock.Object);
            sut.Materialize();
            classFilterMock.Verify(f=>f.Materialize(),Times.Once);
        }

        [TestMethod()]
        public void Negate_Should_NegateFilter()
        {
            var classFilterMock = new Mock<IFilter<IClass>>();
            var sut = new ClassToInterfaceFilterMapper(classFilterMock.Object);
            sut.Negate();
            classFilterMock.Verify(f=>f.Negate(),Times.Once);
        }
    }
}