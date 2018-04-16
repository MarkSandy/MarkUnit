using System.Collections.Generic;

namespace MarkUnit
{
    internal interface ITestResultLogger
    {
        void LogTestsPassed();
        void LogTestsFailed(IEnumerable<INamedComponent> errorItems);
    }
}