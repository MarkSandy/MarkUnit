using System;
using System.Collections.Generic;
using System.Linq;

namespace MarkUnit
{
    internal class TestResultLogger<T> : ITestResultLogger<T> where T : INamedComponent
    {
        public void LogTestsPassed()
        {  
            Console.WriteLine("Passed: " + PredicateString.Text);
        }

        public void LogTestsFailed(IEnumerable<T> errorItems)
        {
            Console.WriteLine("Failed: " + PredicateString.Text);
            Console.WriteLine("The following components fail the architectual predicate:");
            Console.WriteLine(string.Join(", ", errorItems.Select(e => e.Name)));

            Console.WriteLine();

        }
    }
}