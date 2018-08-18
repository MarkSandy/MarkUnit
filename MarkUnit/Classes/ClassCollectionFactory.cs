using System.Linq;

namespace MarkUnit.Classes
{
    internal class ClassCollectionFactory : IClassCollectionFactory
    {
        public ClassCondition Create(IClassRuleFactory classRuleFactory, IClassCollector classCollector, bool negate, bool not, string[] exceptions)
        {
            var classes = classCollector.Get().ToArray();
            var classFilter = new FilteredClasses(classes);
            var result = new ClassCondition(classRuleFactory, classFilter, negate);
            return result.AddIgnoreList(exceptions, not) as ClassCondition;
        }
    }
}
