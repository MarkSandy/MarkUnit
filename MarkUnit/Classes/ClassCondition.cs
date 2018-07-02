using System;
using System.Linq;

namespace MarkUnit.Classes
{
    internal class ClassCondition
        : TypeConditionBase<IClass, IClassCollection, IClassTestCondition, IReducedClassCollection>,
            IClassCollection
    {
        public ClassCondition(IClassRuleFactory classRuleFactory, FilteredClasses classFilter, bool negate)
            : base(classFilter)
        {
            FollowUp = this;
            FilterCondition = new ClassFilterCondition(classRuleFactory, this, classFilter, negate);
        }

        public IReducedClassCollection IsDerivedFrom(Type baseType)
        {
            PredicateString.Add($"is derived from {baseType}");
            var predicate = IsDerivedFromPredicate(baseType);
            return AppendCondition(c => predicate(c.ClassType));
        }

        private static Predicate<Type> IsDerivedFromPredicate(Type classType)
        {
            Predicate<Type> predicate;
            if (classType.IsGenericType)
                predicate = classType.IsSubclassOfRawGeneric;
            else
                predicate = classType.IsSubClass;

            return predicate;
        }

        public IReducedClassCollection ImplementsInterface<TInterface>()
        {
            PredicateString.Add($"implements '{typeof(TInterface)}'");
            return AppendCondition(c => c.ClassType.ImplementsInterface(typeof(TInterface)));
        }

        public IReducedClassCollection IsDerivedFrom<TClass>()
            where TClass : class
        {
            return IsDerivedFrom(typeof(TClass));
        }

        public IReducedClassCollection ImplementsInterfaceMatching(string pattern)
        {
            PredicateString.Add($"implements an interface matching '{pattern}'");
            return AppendCondition(c => c.ClassType.GetInterfaces().Any(i => i.Name.Matches(pattern)));
        }
    }

    internal class InterfaceCondition
        : TypeConditionBase<IInterface, IInterfaceCollection, IInterfaceTestCondition, IReducedInterfaceCollection>,
            IInterfaceCollection
    {
        public InterfaceCondition(IInterfaceRuleFactory interfaceRuleFactory, FilteredInterfaces interfaces, bool negate)
            : base(interfaces)
        {
            FollowUp = this;
            FilterCondition = new InterfaceFilterCondition(interfaceRuleFactory, this, interfaces, negate);
        }

        public IReducedInterfaceCollection ImplementsInterface<TInterface>()
        {
            PredicateString.Add($"implements '{typeof(TInterface)}'");
            return AppendCondition(c => c.ClassType.ImplementsInterface(typeof(TInterface)));
        }

        public IReducedInterfaceCollection ImplementsInterfaceMatching(string pattern)
        {
            PredicateString.Add($"implements an interface matching '{pattern}'");
            return AppendCondition(c => c.ClassType.GetInterfaces().Any(i => i.Name.Matches(pattern)));
        }
    }
}