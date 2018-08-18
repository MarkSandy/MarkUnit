using System.Collections.Generic;

namespace MarkUnit
{
    internal interface ITestResultLogger
    {
        void LogTestsFailed(IEnumerable<INamedComponent> errorItems);
        void LogTestsPassed();
    }
}
