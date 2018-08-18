using System;
using System.Linq;

namespace MarkUnit
{
    public static class TypeExtensions
    {
        public static bool ImplementsInterface(this Type t, Type interfaceType)
        {
            return t.GetInterfaces().Any(x => x.GUID == interfaceType.GUID);
        }

        public static bool IsSubClass(this Type baseClass, Type t)
        {
            if (t.GUID == baseClass.GUID)
                return true;
            return t.BaseType != null && baseClass.IsSubClass(t.BaseType);
        }

        public static bool IsSubclassOfRawGeneric(this Type generic, Type toCheck)
        {
            while (toCheck != null && toCheck != typeof(object))
            {
                var currentType = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (generic.GUID == currentType.GUID)
                {
                    return true;
                }

                toCheck = toCheck.BaseType;
            }

            return false;
        }
    }
}
