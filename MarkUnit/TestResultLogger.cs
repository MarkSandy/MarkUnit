using System;
using System.Collections.Generic;
using System.Linq;

namespace MarkUnit
{
    internal class TestResultLogger : ITestResultLogger
    {
        public void LogTestsPassed()
        {
            Console.WriteLine("Passed: " + PredicateString.Text);
        }

        public void LogTestsFailed(IEnumerable<INamedComponent> errorItems)
        {
            Console.WriteLine("Failed: " + PredicateString.Text);
            Console.WriteLine("The following components fail the architectual predicate:");
            Console.WriteLine(string.Join(", ", errorItems.Select(e => e.Name)));

            Console.WriteLine();
        }
    }
}
