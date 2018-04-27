using System;
using MarkUnit;
using MarkUnit.Assemblies;
using MarkUnit.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Tests.MarkUnit.Classes
{
    [TestClass]
    public class ClassMatchingInterfaceConditionFixture
    {
        private Predicate<IInterface> _savedPredicate;

        private readonly Mock<IInterface> _mockClass1 = new Mock<IInterface>();
        private readonly Mock<IInterface> _mockClass2 = new Mock<IInterface>();

        [TestMethod]
        public void HasMatchingName_Should_CompareStringToAnyImplementedInterface()
        {
            var sut = CreateSystemUnderTest();
            sut.HasMatchingName(t => "IInterfaceOfClass1");
            AssertThatSavedPredicateMatchesClass1AndNotClass2();
        }

        [TestMethod]
        public void HasName_Should_RunFilterAgainstAnyImplementedInterface()
        {
            var sut = CreateSystemUnderTest();
            sut.HasName(x => x == nameof(IInterfaceOfClass1));
            AssertThatSavedPredicateMatchesClass1AndNotClass2();
        }

        [TestMethod]
        public void HasNameMatching_Should_MatchStringAgainstAnyImplementedInterface()
        {
            var sut = CreateSystemUnderTest();
            sut.HasNameMatching("*" + nameof(IInterfaceOfClass1).Substring(3));
            AssertThatSavedPredicateMatchesClass1AndNotClass2();
        }

        [TestMethod]
        public void ImplementsInterface_Should_MatchTypeToAnyImplementedInterface()
        {
            var sut = CreateSystemUnderTest();
            sut.ImplementsInterface<IInterfaceOfClass1>();
            AssertThatSavedPredicateMatchesClass1AndNotClass2();
        }

        [TestMethod]
        public void Is_Should_MatchTypeFilterToAnyImplementedInterface()
        {
            var sut = CreateSystemUnderTest();
            sut.Is(t => t == typeof(IInterfaceOfClass1));
            AssertThatSavedPredicateMatchesClass1AndNotClass2();
        }

        [TestMethod]
         public void IsDeclaredInAssembly_Should_MatchAnyInterfaceThatIsDeclaredInAssemblyMatchingFilter()
        {
            // Arrange
            var mockClass1 = new Mock<IInterface>();
            var mockClass2 = new Mock<IInterface>();
            SetupClass<Class1>(mockClass1, "A", 1);
            SetupClass<AssemblyCollector>(mockClass2, "B", 2);
            var sut = CreateSystemUnderTest(mockClass1.Object, mockClass2.Object);

            // Act
            sut.IsDeclaredInAssembly(a => a == typeof(Class1).Assembly);

            // Assert
            Assert.IsTrue(_savedPredicate(mockClass1.Object));
            Assert.IsFalse(_savedPredicate(mockClass2.Object));
        }

        [TestMethod]
          public void IsDeclaredInAssemblyMatching_Should_MatchAnyInterfaceThatIsDeclaredInAssemblyMatchingGivenPattern()
        {
            // Arrange
            var mockClass1 = new Mock<IInterface>();
            var mockClass2 = new Mock<IInterface>();
            SetupClass<Class1>(mockClass1, "A", 1);
            SetupClass<AssemblyCollector>(mockClass2, "B", 2);
            var sut = CreateSystemUnderTest(mockClass1.Object, mockClass2.Object);

            // Act
            string thisAssembly = typeof(Class1).Assembly.FullName.Split(',')[0].Substring(3);
            sut.IsDeclaredInAssemblyMatching("*" + thisAssembly+",*");

            // Assert
            Assert.IsTrue(_savedPredicate(mockClass1.Object));
            Assert.IsFalse(_savedPredicate(mockClass2.Object));
        }

        private void SetupClass<T>(Mock<IInterface> mock, string name, int index)
        {
            mock.SetupGet(c => c.Name).Returns("A");
            var assemblyMock = new Mock<IAssemblyInfo>();
            assemblyMock.SetupGet(a => a.Name).Returns("Assembly" + index);
            var assembly1 = assemblyMock.Object;
            mock.SetupGet(c => c.AssemblyInfo).Returns(assembly1);
            mock.SetupGet(c => c.Name).Returns(name);
            mock.SetupGet(c => c.ClassType).Returns(typeof(T));
        }

        [TestInitialize]
        public void Setup()
        {
            SetupClass<Class1>(_mockClass1, "A", 1);
            SetupClass<Class2>(_mockClass2, "B", 2);
        }

        private void AssertThatSavedPredicateMatchesClass1AndNotClass2()
        {
            Assert.IsTrue(_savedPredicate(_mockClass1.Object));
            Assert.IsFalse(_savedPredicate(_mockClass2.Object));
        }

        private ClassMatchingInterfaceCondition CreateSystemUnderTest()
        {
            return CreateSystemUnderTest(_mockClass1.Object, _mockClass2.Object);
        }
        private ClassMatchingInterfaceCondition CreateSystemUnderTest(IInterface class1, IInterface class2)
        {
            var verifierMock = new Mock<IAssertionVerifier<IInterface>>(MockBehavior.Loose);
            verifierMock.Setup(v => v.AppendCondition(It.IsAny<Predicate<IInterface>>())).Callback<Predicate<IInterface>>(p => _savedPredicate = p);
            var filter = new Filter<IInterface>(new[] {class1, class2});
            verifierMock.SetupGet(v => v.Items).Returns(filter);
            var sut = new ClassMatchingInterfaceCondition(verifierMock.Object);
            return sut;
        }
    }
}