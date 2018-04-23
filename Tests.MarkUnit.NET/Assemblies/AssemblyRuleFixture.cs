﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using MarkUnit.Assemblies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarkUnit;
using MarkUnit.Classes;
using Moq;

namespace Tests.MarkUnit.Assemblies
{
    [TestClass()]
    public class AssemblyRuleFixture
    {
        private Predicate<IAssembly> _savedPredicate;

        private readonly Mock<IAssembly> _mockAssembly1=new Mock<IAssembly>();
        private readonly Mock<IAssembly> _mockAssembly2=new Mock<IAssembly>();

        private AssemblyRule CreateSystemUnderTest()
        {
            CreateSampleAssemblies();
            var verifierMock = new Mock<IAssertionVerifier<IAssembly>>(MockBehavior.Loose);
            verifierMock.Setup(v => v.AppendCondition(It.IsAny<Predicate<IAssembly>>())).Callback<Predicate<IAssembly>>(p => _savedPredicate = p);
            var filter=new FilteredAssemblies(new []{_mockAssembly1.Object, _mockAssembly2.Object});
            verifierMock.SetupGet(v => v.Items).Returns(filter);
            var sut = new AssemblyRule(verifierMock.Object);
            return sut;
        }

        private void CreateSampleAssemblies()
        {
           CreateSampleAssembly(_mockAssembly1,"A");
            CreateSampleAssembly(_mockAssembly2,"B");
        }

        private void CreateSampleAssembly(Mock<IAssembly> mock, string s)
        {
            mock.SetupGet(a => a.Name).Returns(s);
            var m1=new Mock<IAssembly>();
            m1.SetupGet(x => x.Name).Returns("ReferencedFrom" + s);
            IAssembly[] referencedAssemblies={m1.Object};
            mock.SetupGet(a => a.ReferencedAssemblies).Returns(referencedAssemblies);
        }

        [TestMethod()]
        public void ReferenceAssembly_Should_CheckThatReferenceAssemblyMatchesName()
        {
            var sut = CreateSystemUnderTest();
            sut.ReferenceAssembly("ReferencedFromA");
            AssertThatSavedPredicateMatchesAssembly1AndNotAssembly2();
        }

        private void AssertThatSavedPredicateMatchesAssembly1AndNotAssembly2()
        {
            Assert.IsTrue(_savedPredicate.Invoke(_mockAssembly1.Object));
            Assert.IsFalse(_savedPredicate.Invoke(_mockAssembly2.Object));
        }

        [TestMethod()]
        public void HaveName_Should_CheckThatNameMatchesExpression()
        {
            var sut = CreateSystemUnderTest();
            sut.HaveName(x=>x=="A");
            AssertThatSavedPredicateMatchesAssembly1AndNotAssembly2();
        }

        [TestMethod()]
        public void ReferenceAssembliesMatching_Should_CheckThatAnyReferencedAssemblyMatchesPattern()
        {
            var sut = CreateSystemUnderTest();
            sut.ReferenceAssembliesMatching("*FromA");
            AssertThatSavedPredicateMatchesAssembly1AndNotAssembly2();
        }

        [TestMethod()]
        public void ReferenceAssembliesMatching_Should_Should_CheckThatAnyReferencedAssemblyMatchesFilterExpression()
        {
            var sut = CreateSystemUnderTest();
            sut.ReferenceAssembliesMatching(a=>a.Name.Matches("*FromA"));
            AssertThatSavedPredicateMatchesAssembly1AndNotAssembly2();
        }
    }
}