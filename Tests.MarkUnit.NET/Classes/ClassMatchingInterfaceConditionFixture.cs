using Microsoft.VisualStudio.TestTools.UnitTesting;
using MarkUnit.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarkUnit;
using MarkUnit.Assemblies;
using Moq;

namespace Tests.MarkUnit.Classes
{
    [TestClass()]
    public class ClassMatchingInterfaceConditionFixture
    {
        [TestMethod()]
        public void ClassMatchingInterfaceCondition_Should_()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void HasMatchingName_Should_()
        {
            var sut = CreateSystemUnderTest();
            sut.HasNameMatching("IClass1Interface");
            AssertThatSavedPredicateMatchesClass1AndNotClass2();
        }

        [TestMethod()]
        public void HasName_Should_()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void HasNameMatching_Should_()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ImplementsInterface_Should_()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void Is_Should_()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void IsDeclaredInAssembly_Should_()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void IsDeclaredInAssemblyMatching_Should_()
        {
            Assert.Fail();
        }

        private Predicate<IInterface> _savedPredicate = null;

        private readonly Mock<IClass> _mockClass1 = new Mock<IClass>();
        private readonly Mock<IClass> _mockClass2 = new Mock<IClass>();

        void SetupClass<T>(Mock<IClass> mock, string name, int index)
        {
            mock.SetupGet(c => c.Name).Returns("A");
            var assemblyMock=new Mock<IAssembly>();
            assemblyMock.SetupGet(a => a.Name).Returns("Assembly"+index);
            IAssembly assembly1=assemblyMock.Object;
            mock.SetupGet(c => c.Assembly).Returns(assembly1);
            mock.SetupGet(c => c.ClassType).Returns(typeof(T));
            
        }
        [TestInitialize]
        public void Setup()
        {
            SetupClass<Class1>(_mockClass1,"A",1);
            SetupClass<Class1>(_mockClass2, "B", 2);
        }

        private void AssertThatSavedPredicateMatchesClass1AndNotClass2()
        {
            Assert.IsTrue(_savedPredicate( (IInterface)_mockClass1.Object));
            Assert.IsFalse(_savedPredicate( (IInterface)_mockClass2.Object));
        }

        private ClassMatchingInterfaceCondition CreateSystemUnderTest()
        {
            var verifierMock = new Mock<IAssertionVerifier<IInterface>>(MockBehavior.Loose);
            verifierMock.Setup(v => v.AppendCondition(It.IsAny<Predicate<IInterface>>())).Callback<Predicate<IInterface>>(p =>  _savedPredicate = p);
            var filter=new Filter<IInterface>(new []{(IInterface)_mockClass1.Object, (IInterface)_mockClass2.Object});
            verifierMock.SetupGet(v => v.Items).Returns(filter);
            var sut = new ClassMatchingInterfaceCondition(verifierMock.Object);
            return sut;
        }

    }
}