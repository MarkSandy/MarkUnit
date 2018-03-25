using MarkUnit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.MarkUnit.NET
{
    [TestClass]
    public class StringHelperFixture
    {
        [DataTestMethod
         , DataRow("ABC", "", true)
         , DataRow(null, null, true)
         , DataRow(null, "", true)
         , DataRow(null, "A", false)
         , DataRow("ABC", null, true)
         , DataRow("ABC", "A", false)
         , DataRow("ABC", "A*", true)
         , DataRow("ABC", "*A*", true)
         , DataRow("ABC", "*B*", true)
         , DataRow("ABC", "B*", false)
         , DataRow("ABC", "*C", true)
         , DataRow("ABC", "*C*", true)
        ]
        public void Matches_Should_UseAsteriskToDeterminePosition(string text, string pattern, bool expectedResult)
        {
            var matches = text.Matches(pattern);
            Assert.AreEqual(expectedResult, matches);
        }
    }
}