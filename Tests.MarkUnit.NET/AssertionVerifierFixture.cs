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
        private Mock<ITestResultLogger> _loggerMock;
        
        private static IEnumerable<object[]> ParametersThatCauseAnException
        {
            get
            {
                Predicate<TestItem> satisfiesAll = x => x.Value < 6;
                Predicate<TestItem> satisfiesSome = x => x.Value < 3;
                Predicate<TestItem> satisfiesNone = x => x.Value > 5;

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
                Predicate<TestItem> satisfiesAll = x => x.Value < 6;
                Predicate<TestItem> satisfiesNone = x => x.Value > 5;

                yield return new object[] {satisfiesNone, true};
                yield return new object[] {satisfiesAll, false};
            }
        }

        [TestMethod]
        [DynamicData(nameof(ParametersThatCauseAnException))]
        public void AppendCondition_Should_ThrowException_WhenAddingNonMatchingConditions(Predicate<TestItem> condition, bool negate)
        {
            var sut = CreateSystemUnderTest(negate);
            sut.AppendCondition(condition);
            sut.Verify();
            _loggerMock.Verify(l=>l.LogTestsFailed(It.IsAny<IEnumerable<TestItem>>()));
        }

        [TestMethod]
        [DynamicData(nameof(ParametersThatDontCauseAnException))]
        public void Negate_Should_AllowInverseCondition(Predicate<TestItem> condition, bool negate)
        {
            var sut = CreateSystemUnderTest(negate);
            Predicate<TestItem> inverseCondition = x => !condition(x);
            sut.Negate();
            sut.AppendCondition(inverseCondition);
            sut.Verify();
        }

        [TestMethod]
        [DynamicData(nameof(ParametersThatCauseAnException))]
        public void Negate_Should_ThrowExceptionOnInverseCondition(Predicate<TestItem> condition, bool negate)
        {
            var sut = CreateSystemUnderTest(negate);
            Predicate<TestItem> inverseCondition = x => !condition(x);
            sut.Negate();
            sut.AppendCondition(inverseCondition);
            sut.Verify();
            _loggerMock.Verify(l=>l.LogTestsFailed(It.IsAny<IEnumerable<TestItem>>()));
        }

        [TestMethod]
        [DynamicData(nameof(ParametersThatDontCauseAnException))]
        public void Verify_Should_PassValidConditions(Predicate<TestItem> condition, bool negate)
        {
            var sut = CreateSystemUnderTest(negate);
            sut.AppendCondition(condition);
            sut.Verify();
        }

        private IAssertionVerifier<TestItem> CreateSystemUnderTest(bool negate)
        {
            var filterMock = new Mock<IFilter<TestItem>>(MockBehavior.Strict);
            var items = new[] {new TestItem(1), new TestItem(2), new TestItem(3), new TestItem(4), new TestItem(5)};
            filterMock.Setup(f => f.FilteredItems).Returns(items);
            var filter = filterMock.Object;
            var assertions = new Filter<TestItem>(filter.FilteredItems);
            _loggerMock = new Mock<ITestResultLogger>(MockBehavior.Loose);
            return new AssertionVerifier<TestItem>(filter, assertions, negate, _loggerMock.Object);
        }
    }

    public class TestItem : INamedComponent
    {
        public TestItem(int value)
        {
            Value = value;
        }

        public string Name => Value.ToString();
        public int Value { get; }
    }
}
