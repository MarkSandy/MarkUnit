using System.Collections.Generic;

namespace MarkUnit
{
    internal interface ITestResultLogger<T> 
    {
        void LogTestsPassed();
        void LogTestsFailed(IEnumerable<T> errorItems);
    }
}