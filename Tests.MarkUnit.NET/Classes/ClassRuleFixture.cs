using Microsoft.VisualStudio.TestTools.UnitTesting;
using MarkUnit.Classes;
using System;
using MarkUnit;
using MarkUnit.Assemblies;
using Moq;

namespace Tests.MarkUnit.Classes
{
    [TestClass()]
    public class ClassRuleFixture
    {
        private Predicate<IClass> _savedPredicate = null;

        private readonly Mock<IClass> _mockClass1 = new Mock<IClass>();
        private readonly Mock<IClass> _mockClass2 = new Mock<IClass>();

        [TestInitialize]
        public void Setup()
        {
            _mockClass1.SetupGet(c => c.Name).Returns("A");
            var assemblyMock1=new Mock<IAssembly>();
            assemblyMock1.SetupGet(a => a.Name).Returns("Assembly1");
            IAssembly assembly1=assemblyMock1.Object;
            _mockClass1.SetupGet(c => c.Assembly).Returns(assembly1);
            _mockClass1.SetupGet(c => c.ClassType).Returns(typeof(Class1));
            _mockClass1.SetupGet(c => c.ReferencedNameSpaces).Returns(new[] {"ReferencedNameSpace1"});
            _mockClass1.SetupGet(c => c.Namespace).Returns("NameSpace1");
            _mockClass2.SetupGet(c => c.Name).Returns("AB");
            var assemblyMock2=new Mock<IAssembly>();
            assemblyMock2.SetupGet(a => a.Name).Returns("Assembly2");
            IAssembly assembly2=assemblyMock2.Object;
            _mockClass2.SetupGet(c => c.ClassType).Returns(typeof(Class2));
            _mockClass2.SetupGet(c => c.Assembly).Returns(assembly2);
            _mockClass2.SetupGet(c => c.ReferencedNameSpaces).Returns(new[] {"ReferencedNameSpace2"});
            _mockClass2.SetupGet(c => c.Namespace).Returns("NameSpace2");

            _mockClass1.SetupGet(c => c.ReferencedClasses).Returns(new[] {_mockClass2.Object});

        }

        [TestMethod]
        public void ImplementInterfaceMatching_Should_AppendConditionToVerifier()
        {
            var sut = CreateSystemUnderTest();
            sut.ImplementInterfaceMatching(nameof(IInterfaceOfClass1));
            AssertThatSavedPredicateMatchesClass1AndNotClass2();
        }
        [TestMethod()]
        public void HaveNameMatching_Should_AppendConditionToVerifier()
        {
            var sut = CreateSystemUnderTest();
            sut.HaveNameMatching("A");
            AssertThatSavedPredicateMatchesClass1AndNotClass2();
        }


        private void AssertThatSavedPredicateMatchesClass1AndNotClass2()
        {
            Assert.IsTrue(_savedPredicate(_mockClass1.Object));
            Assert.IsFalse(_savedPredicate(_mockClass2.Object));
        }

        private ClassRule CreateSystemUnderTest()
        {
            var verifierMock = new Mock<IAssertionVerifier<IClass>>(MockBehavior.Loose);
            verifierMock.Setup(v => v.AppendCondition(It.IsAny<Predicate<IClass>>())).Callback<Predicate<IClass>>(p => _savedPredicate = p);
            var filter=new FilteredClasses(new []{_mockClass1.Object, _mockClass2.Object});
            verifierMock.SetupGet(v => v.Items).Returns(filter);
            var sut = new ClassRule(verifierMock.Object);
            return sut;
        }


        [TestMethod()]
        public void HaveName_Should_AppendConditionToVerifier()
        {
            var sut = CreateSystemUnderTest();
            sut.HaveName(n => n == "A");
            AssertThatSavedPredicateMatchesClass1AndNotClass2();
        }

        [TestMethod()]
        public void ReferenceNamespacesMatching_Should_AppendConditionToVerifier()
        {
            var sut = CreateSystemUnderTest();
            sut.ReferenceNamespacesMatching("ReferencedNameSpace1");
            AssertThatSavedPredicateMatchesClass1AndNotClass2();
        }

        [TestMethod()]
        public void ImplementInterfaceGeneric_Should_AppendConditionToVerifier()
        {
            var sut = CreateSystemUnderTest();
            sut.ImplementInterface<IInterfaceOfClass1>();
            AssertThatSavedPredicateMatchesClass1AndNotClass2();
        }

        [TestMethod()]
        public void ImplementInterface_Should_AppendConditionToVerifier()
        {
            var f=new FilteredClasses(new []{_mockClass1.Object});
            var sut = new ClassRule(f,false);
            sut.ImplementInterface().That().HasNameMatching(nameof(IInterfaceOfClass1)).Check();
        }

        [TestMethod()]
        public void UsesClassMatching_Should_AppendConditionToVerifier()
        {
            var sut = CreateSystemUnderTest();
            sut.UsesClassMatching("(\\w+)[0-1]", "$12");
            AssertThatSavedPredicateMatchesClass1AndNotClass2();
        }

        [TestMethod()]
        public void BeInAssemblyMatching_Should_AppendConditionToVerifier()
        {
            var sut = CreateSystemUnderTest();
            sut.BeInAssemblyMatching("Assembly1");
            AssertThatSavedPredicateMatchesClass1AndNotClass2();
        }

        [TestMethod()]
        public void BeDeclaredInNamespaceMatching_Should_AppendConditionToVerifier()
        {
            var sut = CreateSystemUnderTest();
            sut.BeDeclaredInNamespaceMatching("NameSpace1");
            AssertThatSavedPredicateMatchesClass1AndNotClass2();
        }

        [TestMethod()]
        public void Be_Should_AppendConditionToVerifier()
        {
            var sut = CreateSystemUnderTest();
            sut.Be(c => c==typeof(Class1));
            AssertThatSavedPredicateMatchesClass1AndNotClass2();
        }
    }

    internal class Class1 : IInterfaceOfClass1
    {
        private Class2 _class2 = new Class2();
    }
    internal interface IBaseInterface {}
    internal interface IInterfaceOfClass1 : IBaseInterface
    {
    }
    internal class Class2 : IInterfaceOfClass2{}
    internal interface IInterfaceOfClass2
    {
    }
}