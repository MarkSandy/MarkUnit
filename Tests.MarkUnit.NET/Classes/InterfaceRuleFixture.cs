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
    [TestClass]
    public class InterfaceRuleFixture
    { private Predicate<IInterface> _savedPredicate = null;

        private readonly Mock<IInterface> _mockClass1 = new Mock<IInterface>();
        private readonly Mock<IInterface> _mockClass2 = new Mock<IInterface>();

        private void SetupInterface<T>(Mock<IInterface> mock,   int index)
        {
            mock.SetupGet(c => c.Name).Returns("A");
            var assemblyMock = new Mock<IAssemblyInfo>();
            assemblyMock.SetupGet(a => a.Name).Returns("Assembly" + index);
            assemblyMock.SetupGet(a => a.Assembly).Returns(new AssemblyWrapper(typeof(T).Assembly));
            var assembly1 = assemblyMock.Object;
            mock.SetupGet(c => c.AssemblyInfo).Returns(assembly1);
            mock.SetupGet(c => c.Name).Returns(typeof(T).Name);
            mock.SetupGet(c => c.ClassType).Returns(typeof(T));
        }

        
        private void AssertThatSavedPredicateMatchesClass1AndNotClass2()
        {
            Assert.IsTrue(_savedPredicate(_mockClass1.Object));
            Assert.IsFalse(_savedPredicate(_mockClass2.Object));
        }

        [TestInitialize]
        public void Setup()
        {
            SetupInterface<IInterfaceOfClass1>(_mockClass1,  1);
            SetupInterface<IInterfaceOfClass2>(_mockClass2,  2);
        }
        private InterfaceRule CreateSystemUnderTest()
        {
            return CreateSystemUnderTest(_mockClass1.Object, _mockClass2.Object);
        }

        private InterfaceRule CreateSystemUnderTest(IInterface class1, IInterface class2)
        {
            var verifierMock = new Mock<IAssertionVerifier<IInterface>>(MockBehavior.Loose);
            verifierMock.Setup(v => v.AppendCondition(It.IsAny<Predicate<IInterface>>())).Callback<Predicate<IInterface>>(p => _savedPredicate = p);
            var filter = new Filter<IInterface>(new[] {class1, class2});
            verifierMock.SetupGet(v => v.Items).Returns(filter);
            var sut = new InterfaceRule(verifierMock.Object);
            return sut;
        }
       
        [TestMethod]
        public void HasNameMatching_Should_MatchNameAgainstPattern()
        {
            var sut = CreateSystemUnderTest();
            sut.HasNameMatching("*" + nameof(IInterfaceOfClass1).Substring(2));
            AssertThatSavedPredicateMatchesClass1AndNotClass2();
        }
         
        [TestMethod]
        public void HasName_Should_MatchNameAgainstExpression()
        {
            var sut = CreateSystemUnderTest();
            sut.HasName(s=>s==nameof(IInterfaceOfClass1));
            AssertThatSavedPredicateMatchesClass1AndNotClass2();
        }
        [TestMethod]
        public void ImplementsInterface_Should_CheckInterfaceImplementation()
        {
            var sut = CreateSystemUnderTest();
            sut.ImplementsInterface<IBaseInterface>();
            AssertThatSavedPredicateMatchesClass1AndNotClass2(); 
        }

        [TestMethod]
        public void IsDeclaredInAssembly_Should_MatchAnyInterfaceThatIsDeclaredInAssemblyMatchingFilter()
        {
            // Arrange
            var mockClass1 = new Mock<IInterface>();
            var mockClass2 = new Mock<IInterface>();
            SetupInterface<IInterfaceOfClass1>(mockClass1,  1);
            SetupInterface<IMocked>(mockClass2,  2);
            var sut = CreateSystemUnderTest(mockClass1.Object, mockClass2.Object);

            // Act
            sut.IsDeclaredInAssembly(a => a ==typeof(IInterfaceOfClass1).Assembly );

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
            SetupInterface<IInterfaceOfClass1>(mockClass1,  1);
            SetupInterface<IMocked>(mockClass2,  2);
            var sut = CreateSystemUnderTest(mockClass1.Object, mockClass2.Object);

            // Act
            sut.IsDeclaredInAssemblyMatching("*ssembly1");

            // Assert
            Assert.IsTrue(_savedPredicate(mockClass1.Object));
            Assert.IsFalse(_savedPredicate(mockClass2.Object));
        }

        [TestMethod]
        public void Is_Should_()
        {
            var sut = CreateSystemUnderTest();
            sut.Is(t => t == typeof(IInterfaceOfClass1));
            AssertThatSavedPredicateMatchesClass1AndNotClass2(); 
 
        }
    }
}