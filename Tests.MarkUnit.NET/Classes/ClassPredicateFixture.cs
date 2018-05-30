using Microsoft.VisualStudio.TestTools.UnitTesting;
using MarkUnit.Classes;
using Moq;

namespace Tests.MarkUnit.Classes
{
    [TestClass()]
    public class ClassPredicateFixture
    {
        [TestMethod]
        public void That_Should_CreateClassCollectionFromFactory()
        {
            var mock = new Mock<IClassCollector>();
            var classCollector=mock.Object;
            bool negate=false;
            bool not=true;
            var classCollectionFactoryMock=new Mock<IClassCollectionFactory>();
            var sut = new ClassPredicate(classCollector, negate, not,classCollectionFactoryMock.Object);
            sut.That();
            classCollectionFactoryMock.Verify(x=>x.Create(It.IsAny<IClassRuleFactory>(), classCollector,negate,not),Times.Once);
        }

        [TestMethod]
        public void Which_Should_CreateClassCollectionFromFactory()
        {
            var mock = new Mock<IClassCollector>();
            var classCollector=mock.Object;
            bool negate=false;
            bool not=true;
            var classCollectionFactoryMock=new Mock<IClassCollectionFactory>();
            var sut = new ClassPredicate(classCollector, negate, not,classCollectionFactoryMock.Object);
            sut.Which();
            classCollectionFactoryMock.Verify(x=>x.Create(It.IsAny<IClassRuleFactory>(),classCollector,negate,not),Times.Once);
        }
    }
}