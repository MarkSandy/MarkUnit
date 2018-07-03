using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MarkUnit
{
    internal class TestResultExceptionLogger : ITestResultLogger
    {
        public void LogTestsPassed()
        {
            Console.WriteLine("Passed: " + PredicateString.Text);
            ShowWarnings();
        }

        public void LogTestsFailed(IEnumerable<INamedComponent> errorItems)
        {
            Console.WriteLine("Failed: " + PredicateString.Text);
            Console.WriteLine("The following components fail the architectual predicate:");
            Console.WriteLine(string.Join(", ", errorItems.Select(e => e.Name)));

            Console.WriteLine();
            ShowWarnings();
            throw new TestNotPassedException();
        }

        private void ShowWarnings()
        {
            if (PredicateString.Warnings.Any())
            {
                Console.WriteLine("The following warnings were issued:");
                Console.WriteLine(string.Join(Environment.NewLine + "  ", PredicateString.Warnings));
                Assert.Inconclusive();
            }
        }

    }
}