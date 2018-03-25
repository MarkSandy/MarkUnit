using System.Collections.Generic;

namespace MarkUnit
{
    internal interface ITestResultLogger<T> where T : INamedComponent
    {
        void LogTestsPassed();
        void LogTestsFailed(IEnumerable<T> errorItems);
    }
}