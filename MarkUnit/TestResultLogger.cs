using System;
using System.Collections.Generic;
using System.Linq;

namespace MarkUnit
{
    internal class TestResultLogger : ITestResultLogger
    {
        public void LogTestsPassed()
        {
            ShowWarnings();
            Console.WriteLine("Passed: " + PredicateString.Text);
        }

        public void LogTestsFailed(IEnumerable<INamedComponent> errorItems)
        {
            ShowWarnings();
            Console.WriteLine("Failed: " + PredicateString.Text);
            Console.WriteLine("The following components fail the architectual predicate:");
            Console.WriteLine(string.Join(", ", errorItems.Select(e => e.Name)));

            Console.WriteLine();
        }

        private void ShowWarnings()
        {
            if (PredicateString.Warnings.Any())
            {
                Console.WriteLine("The following warnings were issued:");
                Console.WriteLine(string.Join(Environment.NewLine + "  ", PredicateString.Warnings));
            }
        }
    }
}
