using System;
using System.Linq;

namespace NNUG.Prosjekt.Client.Events
{
    public static class TypeExtensions
    {
        public static bool ImplementsInterfaceTemplate2(this Type pluggedType, Type templateType)
        {
            if (!pluggedType.IsConcrete())
            {
                return false;
            }

            return pluggedType.GetInterfaces().Any(interfaceType => ImplementedByGenericType(interfaceType, templateType));
        }

        public static bool ImplementedByGenericType(this Type interfaceType, Type templateType)
        {
            return interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == templateType;
        }

        public static bool IsConcrete(this Type type)
        {
            return !type.IsAbstract && !type.IsInterface;
        }
    }
}