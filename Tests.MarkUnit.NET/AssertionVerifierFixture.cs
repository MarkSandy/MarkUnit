using System;
using System.Collections.Generic;
using MarkUnit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Tests.MarkUnit
{
    [TestClass]
    public class AssertionVerifierFixture
    {
        private Mock<ITestResultLogger<int>> _loggerMock;

        private static IEnumerable<object[]> ParametersThatCauseAnException
        {
            get
            {
                Predicate<int> satisfiesAll = x => x < 6;
                Predicate<int> satisfiesSome = x => x < 3;
                Predicate<int> satisfiesNone = x => x > 5;

                yield return new object[] {satisfiesNone, false};
                yield return new object[] {satisfiesSome, true};
                yield return new object[] {satisfiesSome, false};
                yield return new object[] {satisfiesAll, true};
            }
        }

        private static IEnumerable<object[]> ParametersThatDontCauseAnException
        {
            get
            {
                Predicate<int> satisfiesAll = x => x < 6;
                Predicate<int> satisfiesNone = x => x > 5;

                yield return new object[] {satisfiesNone, true};
                yield return new object[] {satisfiesAll, false};
            }
        }

        [TestMethod]
        [DynamicData(nameof(ParametersThatCauseAnException))]
        public void AppendCondition_Should_ThrowException_WhenAddingNonMatchingConditions(Predicate<int> condition, bool negate)
        {
            var sut = CreateSystemUnderTest(negate);
            sut.AppendCondition(condition);
            sut.Verify();
            _loggerMock.Verify(l=>l.LogTestsFailed(It.IsAny<IEnumerable<int>>()));
        }

        [TestMethod]
        [DynamicData(nameof(ParametersThatDontCauseAnException))]
        public void Negate_Should_AllowInverseCondition(Predicate<int> condition, bool negate)
        {
            var sut = CreateSystemUnderTest(negate);
            Predicate<int> inverseCondition = x => !condition(x);
            sut.Negate();
            sut.AppendCondition(inverseCondition);
            sut.Verify();
        }

        [TestMethod]
        [DynamicData(nameof(ParametersThatCauseAnException))]
        public void Negate_Should_ThrowExceptionOnInverseCondition(Predicate<int> condition, bool negate)
        {
            var sut = CreateSystemUnderTest(negate);
            Predicate<int> inverseCondition = x => !condition(x);
            sut.Negate();
            sut.AppendCondition(inverseCondition);
        sut.Verify();
            _loggerMock.Verify(l=>l.LogTestsFailed(It.IsAny<IEnumerable<int>>()));
        }

        [TestMethod]
        [DynamicData(nameof(ParametersThatDontCauseAnException))]
        public void Verify_Should_PassValidConditions(Predicate<int> condition, bool negate)
        {
            var sut = CreateSystemUnderTest(negate);
            sut.AppendCondition(condition);
            sut.Verify();
        }

        AssertionVerifier<int> CreateSystemUnderTest(bool negate)
        {
            var filterMock = new Mock<IFilter<int>>(MockBehavior.Strict);
            filterMock.Setup(f => f.FilteredItems).Returns(new[] {1, 2, 3, 4, 5});
            var items = filterMock.Object;
            var assertions = new Filter<int>(items.FilteredItems);
            _loggerMock = new Mock<ITestResultLogger<int>>(MockBehavior.Loose);
            return new AssertionVerifier<int>(items, assertions, negate, _loggerMock.Object);
        }
    }
}
